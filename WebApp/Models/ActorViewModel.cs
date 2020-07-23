using Domain.Entities.Model;
using System;
using System.ComponentModel.DataAnnotations;
using Lang = Binit.Framework.Localization.LocalizationConstants.WebApp.Models.ActorViewModel;

namespace WebApp.Models
{
    public class ActorViewModel : EntityViewModel
    {
        [Required(ErrorMessage = Lang.NameRequired)]
        [Display(Name = Lang.NameLabel)]
        public string Name { get; set; }

        public ActorViewModel()
        {

        }

        public ActorViewModel(Actor actor)
        {
            this.Id = actor.Id.ToString();
            this.Name = actor.Name;
        }

        public Actor ToEntity()
        {
            return new Actor()
            {
                Id = this.Id != null ? new Guid(this.Id) : Guid.Empty,
                Name = this.Name
            };
        }
    }
}