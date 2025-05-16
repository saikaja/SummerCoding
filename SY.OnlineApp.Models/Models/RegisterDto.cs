using System;
using System.ComponentModel.DataAnnotations;

namespace SY.OnlineApp.Models.Dtos
{
    public class RegisterDto
    {
        [Required]
        public string UserName { get; set; } = string.Empty;

        [Required]
        public string FirstName { get; set; } = string.Empty;

        [Required]
        public string LastName { get; set; } = string.Empty;

        [Required, EmailAddress]
        public string Email { get; set; } = string.Empty;

        [Required, DataType(DataType.Date)]
        public DateTime DateOfBirth { get; set; }

        [Required]
        public string AddressLine1 { get; set; } = string.Empty;

        public string? AddressLine2 { get; set; }

        [Required]
        public string City { get; set; }

        [Required]
        public string PostalCode { get; set; }
    }
}
