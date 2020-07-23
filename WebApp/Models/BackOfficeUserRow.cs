using Binit.Framework;
using Binit.Framework.Interfaces.DAL;
using Domain.Entities.Model;
using Microsoft.Extensions.Localization;
using System.Collections.Generic;
using WebApp.WebTools;
using WebApp.WebTools.DataTable;
using Lang = Binit.Framework.Localization.LocalizationConstants.WebApp.Models.BackOfficeUserRow;

namespace WebApp.Models
{
    public class BackOfficeUserRow : DataTableRow
    {
        public string Name { get; set; }

        public string LastName { get; set; }

        public string Email { get; set; }

        public string PhoneNumber { get; set; }

        public string JobTitle { get; set; }


        private readonly IStringLocalizer<SharedResources> localizer;
        private readonly IOperationContext operationContext;

        public BackOfficeUserRow() { }
        public BackOfficeUserRow(IStringLocalizer<SharedResources> localizer, IOperationContext operationContext)
        {
            this.localizer = localizer;
            this.operationContext = operationContext;
        }

        public void SetActions(ApplicationUser u)
        {
            this.Actions = new List<DataTableAction>() {
                new DataTableRedirectAction()
                {
                    InternalName = "details",
                    Icon = "eye",
                    Class = "btn-success",
                    Url = "BackOfficeUser/Details/" + u.Id
                },
                new DataTableRedirectAction()
                {
                    InternalName = "edit",
                    Icon = "edit",
                    Class = "btn-primary",
                    Url = "BackOfficeUser/Edit/" + u.Id

                }
            };

            // Prevent users to delete themselves
            if (this.operationContext.GetUserId().Equals(u.Id))
            {
                this.Actions.Add(
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
                        Url = "BackOfficeUser/Delete/" + u.Id,
                        Type = "GET",
                        SuccessTitle = localizer[Lang.DeleteConfirmationMessage]
                    }
                );
            }

            this.Actions.Add(
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
            );
        }
    }
}