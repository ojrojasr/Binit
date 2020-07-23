using Domain.Entities.Model;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Lang = Binit.Framework.Localization.LocalizationConstants.WebApp.Models.PeliculaViewModel;

namespace WebApp.Models
{
    public class PeliculaViewModel : EntityViewModel
    {

        [Required(ErrorMessage = Lang.NameRequired)]
        [Display(Name = Lang.NameLabel)]
        public string Name { get; set; }
        [Required(ErrorMessage = Lang.DescriptionRequired)]
        [StringLength(120, ErrorMessage = Lang.DescriptionStringLength, MinimumLength = 20)]
        [Display(Name = Lang.DescriptionLabel)]
        public string Description { get; set; }
        [Display(Name = Lang.GeneroLabel)]
        public string GeneroId { get; set; }
        [Display(Name = Lang.GeneroLabel)]
        public string Genero { get; set; }
        [Display(Name = Lang.ActoresIdsLabel)]
        public List<string> ActoresIds { get; set; }
        [Display(Name = Lang.ActoresLabel)]
        public List<SelectListItem> Actores { get; set; }
        [Display(Name = Lang.CuriosidadLabel)]
        public List<CuriosidadViewModel> Curiosidades { get; set; }

        public PeliculaViewModel()
        {
            this.Id = new Guid().ToString();
            this.Actores = new List<SelectListItem>();
            this.ActoresIds = new List<string>();
            this.Curiosidades = new List<CuriosidadViewModel>();
            
        }

        public PeliculaViewModel(Pelicula pelicula)
        {
            this.Id = pelicula.Id.ToString();
            this.Name = pelicula.Name;
            this.Description = pelicula.Description;
            this.Actores = new List<SelectListItem>();
            this.Curiosidades = new List<CuriosidadViewModel>();

            if (pelicula.Id != null)
            {
                this.GeneroId = pelicula.generoId.ToString();
                this.Genero = pelicula.genero.Name;
            }

            if (pelicula.Actores != null && pelicula.Actores.Count > 0)
            {
                foreach (var item in pelicula.Actores)
                {
                    this.Actores.Add(new SelectListItem(item.Actor.Name, item.ActorId.ToString(), true));
                }
            }

            if (pelicula.Curiosidades != null && pelicula.Curiosidades.Count > 0)
            {
                foreach (var curiosidad in pelicula.Curiosidades)
                {
                    this.Curiosidades.Add(new CuriosidadViewModel(curiosidad));
                }
            }

        }

        public Pelicula ToEntity()
        {
            var pelicula = new Pelicula()
            {
                Id = this.Id != null ? new Guid(this.Id) : Guid.Empty,
                Name = this.Name,
                Description = this.Description,
                Actores = new List<PeliculaActor>(),
                Curiosidades = new List<Curiosidad>()
                
            };

            // Set product category
            if (!string.IsNullOrEmpty(this.GeneroId))
                pelicula.generoId = new Guid(this.GeneroId);

            if (this.ActoresIds != null && this.ActoresIds.Count > 0)
            {
                foreach (var actoresId in this.ActoresIds)
                {
                    pelicula.Actores.Add(new PeliculaActor()
                    {
                        PeliculaId = pelicula.Id,
                        ActorId = new Guid(actoresId)
                    });
                }
            }

            if (this.Curiosidades != null && this.Curiosidades.Count > 0)
            {
                foreach (var curiosidad in this.Curiosidades)
                {
                    pelicula.Curiosidades.Add(new Curiosidad()
                    {
                        Id = new Guid(curiosidad.Id),
                        Name = curiosidad.Name,
                        Description = curiosidad.Description,
                        PeliculaId = pelicula.Id
                    });
                }
            }


            return pelicula;
        }
    }
}