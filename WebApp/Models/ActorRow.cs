using Binit.Framework;
using Binit.Framework.Constants.Authentication;
using Binit.Framework.Interfaces.DAL;
using Domain.Entities.Model;
using Microsoft.Extensions.Localization;
using System.Collections.Generic;
using WebApp.WebTools;
using WebApp.WebTools.DataTable;
using Lang = Binit.Framework.Localization.LocalizationConstants.WebApp.Models.ActorRow;

namespace WebApp.Models
{
    public class ActorRow : DataTableRow
    {
        public string Name { get; set; }
        private readonly IStringLocalizer<SharedResources> localizer;
        private readonly IOperationContext operationContext;

        public ActorRow() { }

        public ActorRow(IStringLocalizer<SharedResources> localizer, IOperationContext operationContext)
        {
            this.localizer = localizer;
            this.operationContext = operationContext;
        }

        public void SetActions(Actor c)
        {
            this.Actions = new List<DataTableAction>() {
                new DataTableRedirectAction()
                {
                    Icon = "eye",
                    Class = "btn-success",
                    Url = "Actor/Details/" + c.Id
                }
            };

            if (this.operationContext.UserIsInAnyRole(Roles.BackofficeSuperAdministrator))
            {
                this.Actions.Add(new DataTableRedirectAction()
                {
                    InternalName = "edit",
                    Icon = "edit",
                    Class = "btn-primary",
                    Url = "Actor/Edit/" + c.Id
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
                    Url = "Actor/Delete/" + c.Id,
                    Type = "GET",
                    SuccessTitle = localizer[Lang.DeleteConfirmationMessage]
                });
            }
        }
    }
}