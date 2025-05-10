using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SY.OnlineApp.Data.Entities
{
    internal class IntegratedStatus
    {
        public int Id { get; set; }

        [ForeignKey("TypeUtil")]
        public required int Integrated_Id { get; set; }
        public required bool IsDataIntegrated { get; set; }
        public IntegratedType? IntegratedType { get; set; } = null!;
    }
}
