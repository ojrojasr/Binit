using Domain.Entities.Model;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using Lang = Binit.Framework.Localization.LocalizationConstants.WebApp.Models.HolidayViewModel;

namespace WebApp.Models
{
    public class HolidayViewModel : EntityViewModel
    {
        [Required(ErrorMessage = Lang.NameRequired)]
        [Display(Name = Lang.NameLabel)]
        public string Name { get; set; }

        [Required(ErrorMessage = Lang.DescriptionRequired)]
        [Display(Name = Lang.DescriptionLabel)]
        [StringLength(120, ErrorMessage = Lang.DescriptionStringLength, MinimumLength = 20)]
        public string Description { get; set; }

        [Display(Name = Lang.MessageLabel)]
        [StringLength(120, ErrorMessage = Lang.MessageStringLength)]
        public string Message { get; set; }

        [Required(ErrorMessage = Lang.StartDateRequired)]
        [Display(Name = Lang.StartDateLabel)]
        public string StartDate { get; set; }

        [Display(Name = Lang.EndDateLabel)]
        public string EndDate { get; set; }

        [Required(ErrorMessage = Lang.ReasonRequired)]
        [Display(Name = Lang.ReasonLabel)]
        public string ReasonId { get; set; }

        [Display(Name = Lang.ReasonLabel)]
        public string Reason { get; set; }

        [Display(Name = Lang.UsersIdsLabel)]
        public List<string> UsersIds { get; set; }

        [Display(Name = Lang.UsersLabel)]
        public List<SelectListItem> Users { get; set; }

        public HolidayViewModel()
        {
            this.Id = new Guid().ToString();
            this.StartDate = DateTime.Now.ToString("dd/MM/yyyy");
            this.EndDate = DateTime.Now.ToString("dd/MM/yyyy");
            this.UsersIds = new List<string>();
            this.Users = new List<SelectListItem>();
        }

        public HolidayViewModel(Holiday holiday)
        {
            this.Id = holiday.Id.ToString();
            this.Name = holiday.Name;
            this.Description = holiday.Description;
            this.Message = holiday.Message;
            this.StartDate = holiday.StartDate.ToString("dd/MM/yyyy");
            this.EndDate = holiday.EndDate.ToString("dd/MM/yyyy");
            this.ReasonId = holiday.ReasonId.ToString();
            this.Reason = holiday.Reason.Name;

            this.Users = new List<SelectListItem>();
            if (holiday.Users != null && holiday.Users.Count > 0)
            {
                foreach (var item in holiday.Users)
                {
                    this.Users.Add(new SelectListItem(item.User.Email, item.User.Id.ToString(), true));
                }
            }
        }


        public Holiday ToEntity()
        {
            var holiday = new Holiday()
            {
                Id = this.Id != null ? new Guid(this.Id) : Guid.Empty,
                Name = this.Name,
                Description = this.Description,
                Message = this.Message,
                StartDate = DateTime.ParseExact(this.StartDate, "dd/MM/yyyy", CultureInfo.InvariantCulture),
                EndDate = DateTime.ParseExact(this.EndDate, "dd/MM/yyyy", CultureInfo.InvariantCulture),
                ReasonId = new Guid(this.ReasonId),
                Users = new List<HolidayUser>()
            };

            // Set holiday users
            if (this.UsersIds != null && this.UsersIds.Count > 0)
            {
                foreach (var holidayUserId in this.UsersIds)
                {
                    holiday.Users.Add(new HolidayUser()
                    {
                        HolidayId = new Guid(this.Id),
                        UserId = new Guid(holidayUserId)
                    });
                }
            }

            return holiday;
        }
    }
}