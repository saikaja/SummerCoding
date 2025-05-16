using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SY.OnlineApp.Data.Entities
{
    public class LastLogin
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [ForeignKey("Register")]
        public int RegistrationId { get; set; }

        [Required]
        public DateTime LoginTimestamp { get; set; }

        public Register Registration { get; set; } = null!;
    }
}
