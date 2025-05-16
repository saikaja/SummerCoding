using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SY.OnlineApp.Data.Entities
{
    public class OneTimePassCode
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [ForeignKey("Registration")]
        public int RegistrationId { get; set; }

        [Required]
        [MaxLength(8)]
        public string OneCode { get; set; } = string.Empty;

        [Required]
        public DateTime ExpirationTime { get; set; }

        public Register Registration { get; set; } = null!;
    }
}
