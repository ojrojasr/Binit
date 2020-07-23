using Binit.Framework;
using Binit.Framework.Constants.Authentication;
using Binit.Framework.ExceptionHandling.Types;
using Binit.Framework.Helpers;
using Binit.Framework.Interfaces.DAL;
using Domain.Entities.Model;
using Domain.Logic.Interfaces;
using Domain.Logic.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Localization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using WebApp.Attributes;
using WebApp.Models;
using WebApp.WebTools.DataTable;
using JsLang = Binit.Framework.Localization.LocalizationConstants.WebApp.Js;
using Lang = Binit.Framework.Localization.LocalizationConstants.WebApp.Controllers.PeliculaController;

namespace WebApp.Controllers
{
    [AuthorizeAnyRoles(Roles.BackofficeSuperAdministrator, Roles.FrontSuperAdministrator)]
    public class PeliculaController : Controller
    {
        private IPeliculaService peliculaService;
        private IService<Genero> generoService;
        private IActorService actorService;
        private readonly IStringLocalizer<SharedResources> localizer;
        private readonly IOperationContext operationContext;


        public PeliculaController(IPeliculaService peliculaService, IService<Genero> generoService,
        IStringLocalizer<SharedResources> localizer, IOperationContext operationContext, IActorService actorService)
        {
            this.peliculaService = peliculaService;
            this.generoService = generoService;
            this.actorService = actorService;
            this.localizer = localizer;
            this.operationContext = operationContext;
        }
        public IActionResult Index()
        {
            ViewData["Title"] = localizer[Lang.IndexTitle];
            // This is required in order to localize datatables.
            ViewData["DatatableResources"] = JsLocalizer.GetLocalizedResources(JsLang.Datatables, this.localizer);
            return View();
        }

        #region Get
        [HttpPost]
        public JsonResult GetAll(DataTableRequest request)
        {
            var peliculas = this.peliculaService.GetAll();
            var searchTerm = request.search.value;
            Expression<Func<Pelicula, bool>> predicate = null;

            // If search term is not empty...
            if (!string.IsNullOrEmpty(searchTerm))
            {
                predicate = p =>
                (
                    p.Name.Contains(searchTerm) ||
                    p.Description.Contains(searchTerm)
                );
            }

            // Order
            Expression<Func<Pelicula, object>> order = null;
            var orderBy = request.order.FirstOrDefault();
            if (orderBy != null)
            {
                var orderByName = request.columns[orderBy.column].data;
                switch (orderByName)
                {
                    case "name": order = o => o.Name; break;
                    case "description": order = o => o.Description; break;
                }
            };

            var response = new DataTableResponse<Pelicula, PeliculaRow>(request, peliculas, (p =>
            {
                var row = new PeliculaRow(this.localizer, this.operationContext)
                {
                    DT_RowId = p.Id.ToString(),
                    Name = p.Name,
                    Description = p.Description
                };

                row.SetActions(p);
                return row;

            }), predicate, order);

            return new JsonResult(response);
        }
        #endregion

        #region Create
        [AuthorizeAnyRoles(Roles.BackofficeSuperAdministrator, Roles.FrontSuperAdministrator)]
        public IActionResult Create()
        {
            // Setup View Title and Mode
            ViewData["Title"] = localizer[Lang.CreateTitle];
            ViewData["Action"] = "Create";

            // Get categories
            ViewData["Generos"] = this.GetGeneros();

            // Return view with empty object 
            return View("CreateOrEdit", new PeliculaViewModel());
        }

        [AuthorizeAnyRoles(Roles.BackofficeSuperAdministrator, Roles.FrontSuperAdministrator)]
        [HttpPost]
        public async Task<IActionResult> Create( PeliculaViewModel pelicula)
        {
            // Check if model is valid
            if (ModelState.IsValid)
            {
                await this.peliculaService.CreateAsync(pelicula.ToEntity());
                return RedirectToAction("Index", "Pelicula");
            }

            // Setup View Title and Mode
            ViewData["Title"] = localizer[Lang.CreateTitle];
            ViewData["Action"] = "Create";

            // Get categories
            ViewData["Generos"] = this.GetGeneros();

            if (pelicula.ActoresIds != null && pelicula.ActoresIds.Count > 0)
            {
                pelicula.Actores = new List<SelectListItem>();
                var actores = await this.actorService.GetMany(pelicula.ActoresIds);
                foreach (var item in actores)
                {
                    pelicula.Actores.Add(new SelectListItem(item.Name, item.Id.ToString(), true));
                }
            }

            return View("CreateOrEdit", pelicula);
        }
        #endregion

        #region Edit
        [AuthorizeAnyRoles(Roles.BackofficeSuperAdministrator, Roles.FrontSuperAdministrator)]
        [HttpGet]
        public IActionResult Edit(string id)
        {
            // Setup View Title and Mode
            ViewData["Title"] = localizer[Lang.EditTitle];
            ViewData["Action"] = "Edit";

            // Get pelicula from database 
            var pelicula = this.peliculaService.GetFull(new Guid(id));

            // Get categories
            ViewData["Generos"] = this.GetGeneros(pelicula.generoId);

            // Return view with pelicula info
            return View("CreateOrEdit", new PeliculaViewModel(pelicula));
        }

        [AuthorizeAnyRoles(Roles.BackofficeSuperAdministrator)]
        [HttpPost]
        public async Task<IActionResult> Edit( PeliculaViewModel pelicula)
        {
            // Check if model is valid
            if (ModelState.IsValid)
            {
                await this.peliculaService.UpdateAsync(pelicula.ToEntity());
                return RedirectToAction("Index", "Pelicula");
            }
            // Setup View Title and Mode
            ViewData["Title"] = localizer[Lang.EditTitle];
            ViewData["Action"] = "Edit";

            // Get categories
            ViewData["Generos"] = this.GetGeneros(new Guid(pelicula.GeneroId));

            if (pelicula.ActoresIds != null && pelicula.ActoresIds.Count > 0)
            {
                pelicula.Actores = new List<SelectListItem>();
                var actores = await this.actorService.GetMany(pelicula.ActoresIds);
                foreach (var item in actores)
                {
                    pelicula.Actores.Add(new SelectListItem(item.Name, item.Id.ToString(), true));
                }
            }

            return View("CreateOrEdit", pelicula);
        }
        #endregion

        #region Details
        public IActionResult Details(string id)
        {
            try
            {
                ViewData["Title"] = localizer[Lang.DetailsTitle];

                // Get pelicula from database 
                var pelicula = this.peliculaService.GetFull(new Guid(id));
                // Return view with pelicula info
                return View(new PeliculaViewModel(pelicula));
            }
            catch (NotFoundException)
            {
                return NotFound();
            }
        }
        #endregion

        #region Delete
        [AuthorizeAnyRoles(Roles.BackofficeSuperAdministrator, Roles.FrontSuperAdministrator)]
        public async Task<JsonResult> Delete(string id)
        {
            string message = string.Empty;

            try
            {
                await this.peliculaService.DeleteAsync(new Guid(id));
                this.HttpContext.Response.StatusCode = 200;
                message = localizer[Lang.DeleteSuccess];
            }
            catch (NotFoundException ex)
            {
                this.HttpContext.Response.StatusCode = 404;
                message = ex.Message;
            }
            catch (UserException ex)
            {
                this.HttpContext.Response.StatusCode = 500;
                message = ex.Message;
            }
            catch (Exception)
            {
                this.HttpContext.Response.StatusCode = 500;
                message = localizer[Lang.DeleteUnexpectedError];
            }

            return new JsonResult(new
            {
                message = message
            });
        }
        #endregion

        private List<SelectListItem> GetGeneros(Guid? selectedGenero = null)
        {
            if (selectedGenero == null)
            {
                return this.generoService.GetAll()
                    .Select(c => new SelectListItem(c.Name, c.Id.ToString())).ToList();
            }
            else
            {
                return this.generoService.GetAll()
                    .Select(c => new SelectListItem(c.Name, c.Id.ToString(), c.Id == selectedGenero)).ToList();
            }
        }

        [HttpGet]
        public async Task<JsonResult> SearchActores(string term)
        {
            var actores = await this.actorService.SearchActorsAsync(term);

            return new JsonResult(new
            {
                results =
                actores.Select(u => new
                {
                    Id = u.Id.ToString(),
                    text = u.Name
                })
            });
        }

        [HttpGet]
        public IActionResult GetCuriosidadRow(int rowsLength)
        {
            var pelicula = new PeliculaViewModel();
            for (int i = 0; i < rowsLength; i++)
            {
                pelicula.Curiosidades.Add(new CuriosidadViewModel());
            }
            return ViewComponent("CuriosidadRow", new { index = rowsLength - 1, model = pelicula });
        }


    }
}