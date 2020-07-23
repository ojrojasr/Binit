using Binit.Framework;
using Binit.Framework.Constants.Authentication;
using Binit.Framework.Interfaces.DAL;
using Domain.Entities.Model;
using Microsoft.Extensions.Localization;
using System.Collections.Generic;
using WebApp.WebTools;
using WebApp.WebTools.DataTable;
using Lang = Binit.Framework.Localization.LocalizationConstants.WebApp.Models.GeneroRow;

namespace WebApp.Models
{
    public class GeneroRow : DataTableRow
    {
        public string Name { get; set; }

        public string Description { get; set; }

        private readonly IStringLocalizer<SharedResources> localizer;
        private readonly IOperationContext operationContext;

        public GeneroRow() { }

        public GeneroRow(IStringLocalizer<SharedResources> localizer, IOperationContext operationContext)
        {
            this.localizer = localizer;
            this.operationContext = operationContext;
        }

        public void SetActions(Genero c)
        {
            this.Actions = new List<DataTableAction>() {
                new DataTableRedirectAction()
                {
                    InternalName = "details",
                    Icon = "eye",
                    Class = "btn-success",
                    Url = "Genero/Details/" + c.Id
                }
            };

            if (this.operationContext.UserIsInAnyRole(Roles.BackofficeSuperAdministrator))
            {
                this.Actions.Add(new DataTableRedirectAction()
                {
                    InternalName = "edit",
                    Icon = "edit",
                    Class = "btn-primary",
                    Url = "Genero/Edit/" + c.Id
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
                    Url = "Genero/Delete/" + c.Id,
                    Type = "GET",
                    SuccessTitle = localizer[Lang.DeleteConfirmationMessage]
                });
            }
        }
    }
}