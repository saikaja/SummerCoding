using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SY.OnlineApp.Data.Entities
{
    public class Liability
    {
        public int Id { get; set; }  
        public string Name { get; set; } = string.Empty;
        public int Value { get; set; }
    }
}
