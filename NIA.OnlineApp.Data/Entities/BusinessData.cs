using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NIA.OnlineApp.Data.Entities
{
    public class BusinessData
    {
        public int Id { get; set; }  // stays in DB, not exposed in API
        public string Name { get; set; } = string.Empty;
        public int Value { get; set; } 
    }

}
