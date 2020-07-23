using Binit.Framework;
using Binit.Framework.Constants.Authentication;
using Binit.Framework.Interfaces.DAL;
using Domain.Entities.Model;
using Microsoft.Extensions.Localization;
using System.Collections.Generic;
using WebApp.WebTools;
using WebApp.WebTools.DataTable;
using Lang = Binit.Framework.Localization.LocalizationConstants.WebApp.Models.HolidayTypeRow;

namespace WebApp.Models
{
    public class HolidayTypeRow : DataTableRow
    {
        public string Name { get; set; }

        public string Description { get; set; }

        private readonly IStringLocalizer<SharedResources> localizer;
        private readonly IOperationContext operationContext;

        public HolidayTypeRow() { }

        public HolidayTypeRow(IStringLocalizer<SharedResources> localizer, IOperationContext operationContext)
        {
            this.localizer = localizer;
            this.operationContext = operationContext;
        }

        public void SetActions(HolidayType h)
        {
            this.Actions = new List<DataTableAction>() {

                new DataTableRedirectAction()
                {
                    InternalName = "details",
                    Icon = "eye",
                    Class = "btn-success",
                    Url = "HolidayType/Details/" + h.Id
                }
            };

            if (this.operationContext.UserIsInAnyRole(Roles.BackofficeSuperAdministrator, Roles.BackofficeHolidayTypeAdministrator))
            {
                this.Actions.Add(new DataTableRedirectAction()
                {
                    InternalName = "edit",
                    Icon = "edit",
                    Class = "btn-primary",
                    Url = "HolidayType/Edit/" + h.Id
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
                    Url = "HolidayType/Delete/" + h.Id,
                    Type = "GET",
                    SuccessTitle = localizer[Lang.DeleteConfirmationMessage]
                });
            }
        }
    }
}