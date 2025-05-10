using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SY.OnlineApp.Data.Entities
{
    public class IntegratedType
    {
        public int Id { get; set; }
        public required string Type { get; set; }
        public required string Description { get; set; }

        public ICollection<IntegratedStatus> IntegratedStatuses { get; set; } = new List<IntegratedStatus>();
    }
}
