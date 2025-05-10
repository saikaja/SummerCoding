using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SY.OnlineApp.Data.Entities
{
     public class IntegratedStatus
    {
        public int Id { get; set; }

        [ForeignKey("IntergratedType")]
        public required int IntegratedId { get; set; }
        public required bool IsDataIntegrated { get; set; }
        public IntegratedType? IntegratedType { get; set; } = null!;
    }
}
