using Binit.Framework;
using Binit.Framework.Constants.Authentication;
using Binit.Framework.Interfaces.DAL;
using Domain.Entities.Model;
using Microsoft.Extensions.Localization;
using System.Collections.Generic;
using WebApp.WebTools;
using WebApp.WebTools.DataTable;
using Lang = Binit.Framework.Localization.LocalizationConstants.WebApp.Models.PeliculaRow;

namespace WebApp.Models
{
    public class PeliculaRow : DataTableRow
    {
        public string Name { get; set; }

        public string Description { get; set; }

        public string Genero { get; set; }
        private readonly IStringLocalizer<SharedResources> localizer;
        private readonly IOperationContext operationContext;

        public PeliculaRow() { }

        public PeliculaRow(IStringLocalizer<SharedResources> localizer, IOperationContext operationContext)
        {
            this.localizer = localizer;
            this.operationContext = operationContext;
        }

        public void SetActions(Pelicula p)
        {
            this.Actions = new List<DataTableAction>() {
                new DataTableRedirectAction()
                {
                    InternalName = "details",
                    Icon = "eye",
                    Class = "btn-success",
                    Url = "Pelicula/Details/" + p.Id
                }
            };

            if (this.operationContext.UserIsInAnyRole(Roles.BackofficeSuperAdministrator))
            {
                this.Actions.Add(new DataTableRedirectAction()
                {
                    InternalName = "edit",
                    Icon = "edit",
                    Class = "btn-primary",
                    Url = "Pelicula/Edit/" + p.Id

                });

                this.Actions.Add(new DataTableDecisionAction()
                {
                    InternalName = "delete",
                    Class = "btn-danger",
                    Icon = "trash",
                    Modal = new SweetAlert(localizer)
                    {
                        Message = string.Format(localizer[Lang.DeleteQuestionMessage], p.Name),
                        TypeOfAlert = "warning"
                    },
                    Url = "Pelicula/Delete/" + p.Id,
                    Type = "GET",
                    SuccessTitle = localizer[Lang.DeleteConfirmationMessage]
                });
            }
        }
    }
}