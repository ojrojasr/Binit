using Domain.Entities.Model;
using System;

namespace WebAPI.DTOs.HolidayDTOs
{
    public class HolidayUserDTO
    {
        public string Id { get; set; }

        public string Email { get; set; }

        public string Name { get; set; }

        public string LastName { get; set; }

        public HolidayUserDTO(ApplicationUser user)
        {
            this.Id = user.Id.ToString();
            this.Email = user.Email;
            this.Name = user.Name;
            this.LastName = user.LastName;
        }

        public HolidayUser ToEntity()
        {
            return new HolidayUser()
            {
                UserId = new Guid(this.Id)
            };
        }
    }
}