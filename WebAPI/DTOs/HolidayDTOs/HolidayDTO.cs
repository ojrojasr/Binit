using Domain.Entities.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using Lang = Binit.Framework.Localization.LocalizationConstants.WebAPI.DTOs.HolidayDTOs.HolidayDTO;

namespace WebAPI.DTOs.HolidayDTOs
{
    public class HolidayDTO
    {
        public string Id { get; set; }

        [Required(ErrorMessage = Lang.NameRequired)]
        public string Name { get; set; }

        [Required(ErrorMessage = Lang.MessageStringLength)]
        public string Message { get; set; }

        [Required(ErrorMessage = Lang.DescriptionRequired)]
        [StringLength(120, ErrorMessage = Lang.DescriptionStringLength, MinimumLength = 20)]
        public string Description { get; set; }

        [Required(ErrorMessage = Lang.ReasonRequired)]
        public Guid HolidayTypeId { get; set; }

        public List<string> UsersIds { get; set; }

        public List<HolidayUserDTO> Users { get; set; }

        [Required(ErrorMessage = Lang.StartDateRequired)]
        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public HolidayDTO()
        {

        }

        public HolidayDTO(Holiday holiday)
        {
            this.Id = holiday.Id.ToString();
            this.Description = holiday.Description;
            this.EndDate = holiday.EndDate;
            this.Message = holiday.Message;
            this.Name = holiday.Name;
            this.StartDate = holiday.StartDate;
            this.Users = holiday.Users.Select(u => new HolidayUserDTO(u.User)).ToList();
            this.UsersIds = this.Users.Select(u => u.Id).ToList();
        }

        public Holiday ToEntity()
        {
            var holiday = new Holiday()
            {
                Id = this.Id != null ? new Guid(this.Id) : new Guid(),
                Description = this.Description,
                EndDate = this.EndDate,
                Message = this.Message,
                Name = this.Name,
                StartDate = this.StartDate,
                ReasonId = this.HolidayTypeId
            };

            // Set Editors.
            if (this.Users != null && this.Users.Any())
            {
                // Setup holidayId to each HolidayUser entity.
                holiday.Users = new List<HolidayUser>();
                foreach (var user in this.Users.Select(u => u.ToEntity()))
                {
                    user.HolidayId = holiday.Id;
                    holiday.Users.Add(user);
                }
            }

            return holiday;
        }
    }
}