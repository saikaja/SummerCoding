using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SY.OnlineApp.Data.Entities
{
    internal class LogEntry
    {
        public int Id { get; set; }
        public string StackTrace { get; set; } = null!;
        public string Message { get; set; } = null!;
        public string Source { get; set; } = null;
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}
