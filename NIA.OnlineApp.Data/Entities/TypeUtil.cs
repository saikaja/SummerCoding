using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.Reflection.Metadata;
using NIA.OnlineApp.Data.Entities;

namespace NIA.OnlineApp.Data.Entities
{
    public class TypeUtil
    {
        [Key]
        public required int Id { get; set; }

        public required string Type { get; set; } = string.Empty;

        public required bool IsActive { get; set; }
        public ICollection<TypeInformation> TypeInformation { get; set; } = new List<TypeInformation>();


    }
}
