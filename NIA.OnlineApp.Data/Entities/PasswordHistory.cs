using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SY.OnlineApp.Data.Entities
{
    public class PasswordHistory
    {
        public int Id { get; set; }

        public int RegistrationId { get; set; }

        public string PasswordHash { get; set; } = null!;

        public DateTime ChangedAt { get; set; }
        
        [ForeignKey("RegistrationId")]
        public Register Registration { get; set; } = null!;


    }
}

