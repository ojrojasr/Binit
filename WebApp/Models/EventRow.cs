using Binit.Framework;
using Binit.Framework.Constants.Authentication;
using Binit.Framework.Interfaces.DAL;
using Domain.Entities.Model;
using Microsoft.Extensions.Localization;
using System.Collections.Generic;
using WebApp.WebTools;
using WebApp.WebTools.DataTable;
using Lang = Binit.Framework.Localization.LocalizationConstants.WebApp.Models.EventRow;

namespace WebApp.Models
{
    public class EventRow : DataTableRow
    {
        public string Name { get; set; }

        public string Description { get; set; }

        private readonly IStringLocalizer<SharedResources> localizer;
        private readonly IOperationContext operationContext;

        public EventRow() { }

        public EventRow(IStringLocalizer<SharedResources> localizer, IOperationContext operationContext)
        {
            this.localizer = localizer;
            this.operationContext = operationContext;
        }

        public void SetActions(Event h)
        {
            this.Actions = new List<DataTableAction>() {

                new DataTableRedirectAction()
                {
                    InternalName = "details",
                    Icon = "eye",
                    Class = "btn-success",
                    Url = "Event/Details/" + h.Id
                }
            };

            if (this.operationContext.UserIsInAnyRole(Roles.BackofficeSuperAdministrator, Roles.BackofficeEventAdministrator))
            {
                this.Actions.Add(new DataTableRedirectAction()
                {
                    InternalName = "edit",
                    Icon = "edit",
                    Class = "btn-primary",
                    Url = "Event/Edit/" + h.Id

                });
                this.Actions.Add(new DataTableDecisionAction()
                {
                    InternalName = "delete",
                    Class = "btn-danger",
                    Icon = "trash",
                    Modal = new SweetAlert(localizer)
                    {
                        Message = string.Format(localizer[Lang.DeleteQuestionMessage], h.Name),
                        TypeOfAlert = "warning"
                    },
                    Url = "Event/Delete/" + h.Id,
                    Type = "GET",
                    SuccessTitle = localizer[Lang.DeleteConfirmationMessage]
                });
            }
        }
    }
}