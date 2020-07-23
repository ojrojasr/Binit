using Domain.Entities.Model;
using System;
using System.ComponentModel.DataAnnotations;

namespace WebAPI.DTOs.AccountDTOs
{
    public class CompleteSocialAuthReq
    {
        public string Name { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }

        [Required]
        public string LoginProvider { get; set; }

        [Required]
        public string ProviderKey { get; set; }

        public ApplicationUser ToEntity()
        {
            var entity = new ApplicationUser
            {
                UserName = this.Email,
                Email = this.Email,
                Name = this.Name,
                LastName = this.LastName,
                SecurityStamp = Guid.NewGuid().ToString(),
                EmailConfirmed = true,
                LockoutEnabled = false
            };

            return entity;
        }
    }
}