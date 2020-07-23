using Binit.Framework;
using Binit.Framework.Constants.Authentication;
using Binit.Framework.Interfaces.DAL;
using Domain.Entities.Model;
using Microsoft.Extensions.Localization;
using System.Collections.Generic;
using WebApp.WebTools;
using WebApp.WebTools.DataTable;
using Lang = Binit.Framework.Localization.LocalizationConstants.WebApp.Models.CategoryRow;

namespace WebApp.Models
{
    public class CategoryRow : DataTableRow
    {
        public string Name { get; set; }

        public string Description { get; set; }

        private readonly IStringLocalizer<SharedResources> localizer;
        private readonly IOperationContext operationContext;

        public CategoryRow() { }

        public CategoryRow(IStringLocalizer<SharedResources> localizer, IOperationContext operationContext)
        {
            this.localizer = localizer;
            this.operationContext = operationContext;
        }

        public void SetActions(Category c)
        {
            this.Actions = new List<DataTableAction>() {
                new DataTableRedirectAction()
                {
                    InternalName = "details",
                    Icon = "eye",
                    Class = "btn-success",
                    Url = "Category/Details/" + c.Id
                }
            };

            if (this.operationContext.UserIsInAnyRole(Roles.BackofficeSuperAdministrator, Roles.BackofficeCategoryAdministrator))
            {
                this.Actions.Add(new DataTableRedirectAction()
                {
                    InternalName = "edit",
                    Icon = "edit",
                    Class = "btn-primary",
                    Url = "Category/Edit/" + c.Id
                });

                this.Actions.Add(new DataTableDecisionAction()
                {
                    InternalName = "delete",
                    Class = "btn-danger",
                    Icon = "trash",
                    Modal = new SweetAlert(localizer)
                    {
                        Message = string.Format(localizer[Lang.DeleteQuestionMessage], c.Name),
                        TypeOfAlert = "warning"
                    },
                    Url = "Category/Delete/" + c.Id,
                    Type = "GET",
                    SuccessTitle = localizer[Lang.DeleteConfirmationMessage]
                });
            }
        }
    }
}