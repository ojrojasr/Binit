using Binit.Framework;
using Binit.Framework.Constants.Authentication;
using Binit.Framework.ExceptionHandling.Types;
using Binit.Framework.Helpers;
using Binit.Framework.Helpers.Email.DTOs;
using Binit.Framework.Interfaces.DAL;
using Binit.Framework.Interfaces.Email;
using Domain.Entities.Model;
using Domain.Logic.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Localization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using System.Web;
using WebApp.Attributes;
using WebApp.Models;
using WebApp.WebTools.DataTable;
using JsLang = Binit.Framework.Localization.LocalizationConstants.WebApp.Js;
using Lang = Binit.Framework.Localization.LocalizationConstants.WebApp.Controllers.UserController;

namespace WebApp.Controllers
{
    [AuthorizeAnyRoles(Roles.BackofficeSuperAdministrator, Roles.BackofficeBackofficeUserAdministrator)]
    public class UserController : Controller
    {
        private readonly IUserService userService;
        protected readonly IAccountService accountService;
        private readonly ITenantService tenantService;
        protected readonly IOperationContext operationContext;
        protected readonly IEmailSender emailSender;
        protected readonly IStringLocalizer<SharedResources> localizer;
        protected readonly IConfiguration configuration;

        public UserController(IUserService userService, IAccountService accountService, ITenantService tenantService,
            IEmailSender emailSender, IStringLocalizer<SharedResources> localizer, IConfiguration configuration, IOperationContext operationContext)
        {
            this.userService = userService;
            this.accountService = accountService;
            this.tenantService = tenantService;
            this.emailSender = emailSender;
            this.localizer = localizer;
            this.configuration = configuration;
            this.operationContext = operationContext;
        }

        public virtual IActionResult Index()
        {
            ViewData["Title"] = localizer[Lang.IndexTitle];
            // This is required in order to localize datatables.
            ViewData["DatatableResources"] = JsLocalizer.GetLocalizedResources(JsLang.Datatables, this.localizer);
            return View();
        }

        #region Get
        [HttpPost]
        public virtual JsonResult GetAll(DataTableRequest request)
        {

            var users = this.userService.GetAll();
            var searchTerm = request.search.value;
            Expression<Func<ApplicationUser, bool>> predicate = null;

            // If search term is not empty...
            if (!string.IsNullOrEmpty(searchTerm))
            {
                predicate = p =>
                (
                    p.Name.Contains(searchTerm) ||
                    p.LastName.Contains(searchTerm) ||
                    p.Email.Contains(searchTerm) ||
                    p.PhoneNumber.Contains(searchTerm)
                );
            }

            // Order
            Expression<Func<ApplicationUser, object>> order = null;
            var orderBy = request.order.FirstOrDefault();
            if (orderBy != null)
            {
                var orderByName = request.columns[orderBy.column].data;
                switch (orderByName)
                {
                    case "name": order = o => o.Name; break;
                    case "lastName": order = o => o.LastName; break;
                    case "email": order = o => o.Email; break;
                    case "phoneNumber": order = o => o.PhoneNumber; break;
                    default: order = o => o.CreatedDate; break;
                }
            };

            var response = new DataTableResponse<ApplicationUser, ApplicationUserRow>(request, users, (u =>
            {
                var row = new ApplicationUserRow(localizer)
                {
                    DT_RowId = u.Id.ToString(),
                    Name = u.Name,
                    LastName = u.LastName,
                    Email = u.Email,
                    PhoneNumber = u.PhoneNumber
                };

                row.SetActions(u);
                return row;

            }), predicate, order);

            return new JsonResult(response);
        }
        #endregion

        #region Create
        [HttpGet]
        public virtual IActionResult Create()
        {
            // Setup View Title and Mode
            ViewData["Title"] = localizer[Lang.CreateTitle];
            ViewData["Action"] = "Create";

            // Get roles
            ViewData["Roles"] = this.GetRoles();

            // Get tenants
            ViewData["Tenants"] = this.GetTenants();

            // Return view with empty object 
            return View("CreateOrEdit", new ApplicationUserViewModel());
        }

        [HttpPost]
        public async Task<IActionResult> Create([Bind("Name,LastName,Email,PhoneNumber,Roles,TenantId")] ApplicationUserViewModel user)
        {
            // Check if model is valid
            if (ModelState.IsValid)
            {
                var code = await this.userService.CreateAsync(user.ToEntity(), user.Roles);
                var encodedCode = HttpUtility.UrlEncode(code);
                var userDB = await this.accountService.GetUser(user.Email);
                // Build callback URL for password creation.
                var callbackUrl = $"{this.Request.Scheme}://{this.Request.Host}/Identity/Account/CreatePassword?userId={userDB.Id.ToString()}&code={encodedCode}";

                // Send confirmation email.
                await emailSender.SendEmailAsync(user.Email, "Confirm your email", new WelcomeDTO(configuration, localizer) { Name = user.Email, CallbackUrl = callbackUrl });

                return RedirectToAction("Index", "User");
            }

            // Get roles
            ViewData["Roles"] = this.GetRoles(user.Roles);

            // Get tenants
            ViewData["Tenants"] = this.GetTenants(user.TenantId);

            // Setup View Title and Mode
            ViewData["Title"] = localizer[Lang.CreateTitle];
            ViewData["Action"] = "Create";

            return View("CreateOrEdit", user);
        }
        #endregion

        #region Edit
        [HttpGet]
        public virtual async Task<IActionResult> Edit(string id)
        {
            // Setup View Title and Mode
            ViewData["Title"] = localizer[Lang.EditTitle];
            ViewData["Action"] = "Edit";

            // Get user from database 
            var user = await this.userService.GetFullAsync(new Guid(id));

            // Get user roles.
            var userRoles = await accountService.GetRoles(user);

            // Get roles
            ViewData["Roles"] = this.GetRoles(userRoles.ToList());

            // Get tenants
            ViewData["Tenants"] = this.GetTenants(user.TenantId.ToString());

            // Return view with user info
            return View("CreateOrEdit", new ApplicationUserViewModel(user));
        }

        [HttpPost]
        public async Task<IActionResult> Edit([Bind("Id,Name,LastName,Email,PhoneNumber,Roles,TenantId")] ApplicationUserViewModel user)
        {
            // Check if model is valid
            if (ModelState.IsValid)
            {
                var userEntity = user.ToEntity();

                await this.userService.UpdateAsync(userEntity, user.Roles);
                return RedirectToAction("Index", "User");
            }

            // Setup View Title and Mode
            ViewData["Title"] = localizer[Lang.EditTitle];
            ViewData["Action"] = "Edit";

            // Get roles
            ViewData["Roles"] = this.GetRoles(user.Roles);

            // Get tenants
            ViewData["Tenants"] = this.GetTenants(user.TenantId);

            return View("CreateOrEdit", user);
        }
        #endregion

        #region Details
        public virtual async Task<IActionResult> Details(string id)
        {
            ViewData["Title"] = localizer[Lang.DetailsTitle];

            // Get user from database 
            var user = await this.userService.GetFullAsync(new Guid(id));
            // Get user roles.
            var userRoles = await accountService.GetRoles(user);

            // Get roles
            ViewData["Roles"] = this.GetRoles(userRoles.ToList());

            // Return view with user info
            return View(new ApplicationUserViewModel(user));
        }
        #endregion

        #region Delete
        public async Task<JsonResult> Delete(string id)
        {
            string message = string.Empty;

            try
            {
                await this.userService.DeleteAsync(new Guid(id));
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

        [HttpPost]
        public async Task<JsonResult> PasswordRecovery(string id)
        {
            string message = string.Empty;
            try
            {
                // Get the user's data.
                var user = await this.userService.GetAsync(new Guid(id));

                // Generate recovery code.
                var code = await accountService.GeneratePasswordToken(user.Email);

                // Generate recovery callback.
                var queryParams = new Dictionary<string, string>();
                queryParams.Add("userId", user.Id.ToString());
                queryParams.Add("code", code);

                var callbackUrl = QueryHelpers.AddQueryString("Identity/Account/CreatePassword", queryParams);

                // Send email.
                await emailSender.SendEmailAsync(user.Email, this.localizer[Lang.PasswordRecoveryEmailSubject],
                new PasswordRecoveryDTO(configuration, localizer) { Name = user.Name, CallbackUrl = callbackUrl });

                this.HttpContext.Response.StatusCode = 200;
                message = localizer[Lang.PasswordRecoverySuccess];
            }
            catch (NotFoundException ex)
            {
                this.HttpContext.Response.StatusCode = 404;
                message = ex.Message;
            }
            catch (Exception)
            {
                this.HttpContext.Response.StatusCode = 500;
                message = localizer[Lang.PasswordRecoveryUnexpectedError];
            }

            return new JsonResult(new
            {
                message = message
            });
        }

        [HttpGet]
        public async Task<JsonResult> SearchTenants(string term)
        {
            var tenants = await this.tenantService.Search(term);

            return new JsonResult(new
            {
                results =
                tenants.Select(u => new
                {
                    id = u.Id.ToString(),
                    text = u.Name
                })
            });
        }

        public virtual List<SelectListItem> GetRoles(List<string> selectedRoles = null)
        {
            return Roles.GetAllRoles()
                .Select(r => new SelectListItem(r, r, selectedRoles != null ? selectedRoles.Contains(r) : false))
                .ToList();
        }

        protected List<SelectListItem> GetTenants(string selectedTenantId = null)
        {
            if (selectedTenantId == null)
            {
                return tenantService.GetAll()
                        .Select(t => new SelectListItem(t.Name, t.Id.ToString()))
                        .ToList();
            }
            else
            {
                return tenantService.GetAll()
                        .Select(t => new SelectListItem(t.Name, t.Id.ToString(), t.Id == new Guid(selectedTenantId)))
                        .ToList();
            }
        }


    }
}