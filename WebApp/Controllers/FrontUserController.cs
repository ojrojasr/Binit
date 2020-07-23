using Binit.Framework;
using Binit.Framework.Constants.Authentication;
using Binit.Framework.Helpers;
using Binit.Framework.Helpers.Email.DTOs;
using Binit.Framework.Interfaces.DAL;
using Binit.Framework.Interfaces.Email;
using Domain.Entities.Model;
using Domain.Logic.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
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
using Lang = Binit.Framework.Localization.LocalizationConstants.WebApp.Controllers.FrontUserController;

namespace WebApp.Controllers
{
    [AuthorizeAnyRoles(Roles.BackofficeSuperAdministrator, Roles.BackofficeFrontUserAdministrator, Roles.FrontSuperAdministrator, Roles.FrontFrontUserAdministrator)]
    public class FrontUserController : UserController
    {
        private readonly IFrontUserService frontUserService;

        public FrontUserController(IUserService userService, IAccountService accountService, ITenantService tenantService,
            IEmailSender emailSender, IStringLocalizer<SharedResources> localizer, IConfiguration configuration,
            IFrontUserService frontUserService, IOperationContext operationContext)
            : base(userService, accountService, tenantService, emailSender, localizer, configuration, operationContext)
        {
            this.frontUserService = frontUserService;
        }

        public override IActionResult Index()
        {
            ViewData["Title"] = base.localizer[Lang.IndexTitle];
            // This is required in order to localize datatables.
            ViewData["DatatableResources"] = JsLocalizer.GetLocalizedResources(JsLang.Datatables, this.localizer);
            return View();
        }

        [HttpPost]
        public override JsonResult GetAll(DataTableRequest request)
        {
            var users = this.frontUserService.GetAll();
            var searchTerm = request.search.value;
            Expression<Func<FrontUser, bool>> predicate = null;

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
            Expression<Func<FrontUser, object>> order = null;
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
                    case "birthdate": order = o => o.Birthdate; break;
                    default: order = o => o.CreatedDate; break;
                }
            };

            var response = new DataTableResponse<FrontUser, FrontUserRow>(request, users, (u =>
            {
                var row = new FrontUserRow(localizer, operationContext)
                {
                    DT_RowId = u.Id.ToString(),
                    Name = u.Name,
                    LastName = u.LastName,
                    Email = u.Email,
                    PhoneNumber = u.PhoneNumber,
                    Birthdate = u.Birthdate.HasValue ? u.Birthdate.Value.ToString("dd/MM/yyyy") : ""
                };

                row.SetActions(u);
                return row;

            }), predicate, order);

            return new JsonResult(response);
        }

        [HttpGet]
        public override IActionResult Create()
        {
            // Setup View Title and Mode
            ViewData["Title"] = localizer[Lang.CreateTitle];
            ViewData["Action"] = "CreateFront";

            // Get roles
            ViewData["Roles"] = this.GetRoles();

            // Get tenants
            ViewData["Tenants"] = base.GetTenants();

            // Return view with empty object 
            return View("CreateOrEdit", new FrontUserViewModel());
        }

        [HttpPost]
        public async Task<IActionResult> CreateFront([Bind("Name,LastName,Email,PhoneNumber,Roles,TenantId,Birthdate")] FrontUserViewModel user)
        {
            // Check if model is valid
            if (ModelState.IsValid)
            {
                var code = await frontUserService.CreateAsync(user.ToEntity(), user.Roles);
                var encodedCode = HttpUtility.UrlEncode(code);
                var userDB = await this.accountService.GetUser(user.Email);
                // Build callback URL for password creation.
                var callbackUrl = $"{this.Request.Scheme}://{this.Request.Host}/Identity/Account/CreatePassword?userId={userDB.Id.ToString()}&code={encodedCode}";

                // Send confirmation email.
                await emailSender.SendEmailAsync(user.Email, "Confirm your email", new WelcomeDTO(configuration, localizer) { Name = user.Email, CallbackUrl = callbackUrl });

                return RedirectToAction("Index", "FrontUser");
            }

            // Get roles
            ViewData["Roles"] = this.GetRoles(user.Roles);

            // Get tenants
            ViewData["Tenants"] = base.GetTenants(user.TenantId);

            // Setup View Title and Mode
            ViewData["Title"] = localizer[Lang.CreateTitle];
            ViewData["Action"] = "CreateFront";

            return View("CreateOrEdit", user);
        }

        [HttpGet]
        public override async Task<IActionResult> Edit(string id)
        {
            // Setup View Title and Mode
            ViewData["Title"] = localizer[Lang.EditTitle];
            ViewData["Action"] = "EditFront";

            // Get user from database 
            var user = await this.frontUserService.GetFullAsync(new Guid(id));

            // Get user roles.
            var userRoles = await accountService.GetRoles(user);

            // Get roles
            ViewData["Roles"] = this.GetRoles(userRoles.ToList());

            // Get tenants
            ViewData["Tenants"] = base.GetTenants(user.TenantId.ToString());

            // Return view with user info
            return View("CreateOrEdit", new FrontUserViewModel(user));
        }

        [HttpPost]
        public async Task<IActionResult> EditFront([Bind("Id,Name,LastName,Email,PhoneNumber,Roles,TenantId,Birthdate,UserType")] FrontUserViewModel user)
        {
            // Check if model is valid
            if (ModelState.IsValid)
            {
                var userEntity = user.ToEntity();

                await this.frontUserService.UpdateAsync(userEntity, user.Roles);
                return RedirectToAction("Index", "FrontUser");
            }

            // Setup View Title and Mode
            ViewData["Title"] = localizer[Lang.EditTitle];
            ViewData["Action"] = "EditFront";

            // Get roles
            ViewData["Roles"] = this.GetRoles(user.Roles);

            // Get tenants
            ViewData["Tenants"] = base.GetTenants(user.TenantId);

            return View("CreateOrEdit", user);
        }

        public override async Task<IActionResult> Details(string id)
        {
            ViewData["Title"] = localizer[Lang.DetailsTitle];

            // Get user from database 
            var user = await this.frontUserService.GetFullAsync(new Guid(id));
            // Get user roles.
            var userRoles = await accountService.GetRoles(user);

            // Get roles
            ViewData["Roles"] = this.GetRoles(userRoles.ToList());

            // Return view with user info
            return View(new FrontUserViewModel(user));
        }

        public override List<SelectListItem> GetRoles(List<string> selectedRoles = null)
        {
            bool userIsSuperAdmin = base.operationContext.UserIsInAnyRole(Roles.FrontSuperAdministrator, Roles.BackofficeSuperAdministrator);
            return Roles.GetRolesByRealm("Front", includeSuperAdmin: userIsSuperAdmin)
                .Select(r => new SelectListItem(r, r, selectedRoles != null ? selectedRoles.Contains(r) : false))
                .ToList();
        }
    }
}