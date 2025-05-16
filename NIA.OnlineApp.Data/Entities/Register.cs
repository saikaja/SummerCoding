using System;
using System.ComponentModel.DataAnnotations;

namespace SY.OnlineApp.Data.Entities
{
    public class Register
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(50)]
        public string UserName { get; set; } = string.Empty;

        [Required]
        [MaxLength(50)]
        public string FirstName { get; set; } = string.Empty;

        [Required]
        [MaxLength(50)]
        public string LastName { get; set; } = string.Empty;

        [Required]
        [EmailAddress]
        [MaxLength(100)]
        public string Email { get; set; } = string.Empty;

        [Required]
        [DataType(DataType.Date)]
        [CustomValidation(typeof(Register), nameof(ValidateAge))]
        public DateTime DateOfBirth { get; set; }

        // Address Fields
        [Required]
        [MaxLength(100)]
        public string AddressLine1 { get; set; } = string.Empty;

        [MaxLength(100)]
        public string? AddressLine2 { get; set; }

        [Required]
        [MaxLength(50)]
        public string City { get; set; }

        [Required]
        [MaxLength(20)]
        public string PostalCode { get; set; }

        public static ValidationResult? ValidateAge(DateTime dateOfBirth, ValidationContext context)
        {
            var age = DateTime.Today.Year - dateOfBirth.Year;
            if (dateOfBirth > DateTime.Today.AddYears(-age)) age--;

            return age >= 18
                ? ValidationResult.Success
                : new ValidationResult("Minimum age is 18.");
        }
    }
}
