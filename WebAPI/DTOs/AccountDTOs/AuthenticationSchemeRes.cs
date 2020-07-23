using System.ComponentModel.DataAnnotations;

namespace WebAPI.DTOs.AccountDTOs
{
    public class AuthenticationSchemeRes
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public string DisplayName { get; set; }
    }
}