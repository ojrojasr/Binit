using Binit.Framework;
using Domain.Entities.Model;
using Microsoft.Extensions.Localization;
using System.Collections.Generic;
using WebApp.WebTools;
using WebApp.WebTools.DataTable;
using Lang = Binit.Framework.Localization.LocalizationConstants.WebApp.Models.TenantRow;

namespace WebApp.Models
{
    public class TenantRow : DataTableRow
    {
        #region Row Columns

        public string Code { get; set; }

        public string Name { get; set; }

        #endregion

        private readonly IStringLocalizer<SharedResources> localizer;


        public TenantRow() { }

        public TenantRow(IStringLocalizer<SharedResources> localizer)
        {
            this.localizer = localizer;
        }

        public void SetActions(Tenant t)
        {
            this.Actions = new List<DataTableAction>() {

                new DataTableRedirectAction()
                {
                    InternalName = "details",
                    Icon = "eye",
                    Class = "btn-success",
                    Url = "Tenant/Details/" + t.Id
                }
            };

            this.Actions.Add(new DataTableRedirectAction()
            {
                InternalName = "edit",
                Icon = "edit",
                Class = "btn-primary",
                Url = "Tenant/Edit/" + t.Id

            });

            this.Actions.Add(new DataTableDecisionAction()
            {
                InternalName = "delete",
                Class = "btn-danger",
                Icon = "trash",
                Modal = new SweetAlert(localizer)
                {
                    Message = string.Format(localizer[Lang.DeleteQuestionMessage], t.Name),
                    TypeOfAlert = "warning"
                },
                Url = "Tenant/Delete/" + t.Id,
                Type = "GET",
                SuccessTitle = localizer[Lang.DeleteConfirmationMessage]
            });

        }
    }
}