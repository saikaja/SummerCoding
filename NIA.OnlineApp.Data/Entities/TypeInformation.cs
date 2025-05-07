using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NIA.OnlineApp.Data.Entities
{
    public class TypeInformation
    {
        [Key]
        public int Id { get; set; }

        [ForeignKey("TypeUtil")]
        public required int Type_Id { get; set; } 
        public required string Name { get; set; }
        public required int Value { get; set; }
        
    }
}
