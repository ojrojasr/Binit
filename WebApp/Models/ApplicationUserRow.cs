using Binit.Framework;
using Domain.Entities.Model;
using Microsoft.Extensions.Localization;
using System.Collections.Generic;
using WebApp.WebTools;
using WebApp.WebTools.DataTable;
using Lang = Binit.Framework.Localization.LocalizationConstants.WebApp.Models.ApplicationUserRow;

namespace WebApp.Models
{
    public class ApplicationUserRow : DataTableRow
    {
        public string Name { get; set; }

        public string LastName { get; set; }

        public string Email { get; set; }

        public string PhoneNumber { get; set; }

        private readonly IStringLocalizer<SharedResources> localizer;

        public ApplicationUserRow() { }
        public ApplicationUserRow(IStringLocalizer<SharedResources> localizer)
        {
            this.localizer = localizer;
        }

        public void SetActions(ApplicationUser u)
        {
            this.Actions = new List<DataTableAction>() {
                    new DataTableRedirectAction()
                    {
                        InternalName = "details",
                        Icon = "eye",
                        Class = "btn-success",
                        Url = "User/Details/" + u.Id
                    },
                    new DataTableRedirectAction()
                    {
                        InternalName = "edit",
                        Icon = "edit",
                        Class = "btn-primary",
                        Url = "User/Edit/" + u.Id

                    },
                    new DataTableDecisionAction()
                    {
                        InternalName = "delete",
                        Class = "btn-danger",
                        Icon = "trash",
                        Modal = new SweetAlert(localizer)
                        {
                            Message = string.Format(localizer[Lang.DeleteQuestionMessage], u.Name, u.LastName),
                            TypeOfAlert = "warning"
                        },
                        Url = "User/Delete/" + u.Id,
                        Type = "GET",
                        SuccessTitle = localizer[Lang.DeleteConfirmationMessage]
                    },
                    new DataTableAction()
                    {
                        InternalName = "passwordRecovery",
                        DisplayName = localizer[Lang.PasswordRecoveryLabel],
                        Class = "btn-secondary",
                        Icon = "key",
                        Url = "User/PasswordRecovery/" + u.Id,
                        Type = "POST",
                        SuccessTitle = string.Format(localizer[Lang.PasswordRecoveryConfirmationMessage], u.GetFullName())
                    }
                };
        }
    }
}