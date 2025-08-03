using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArch_Task.Domain.Entities
{
    public class Ticket
    {
        public int Id { get; set; }  // Identity في DB
        public string Type { get; set; } = null!;
        public decimal Price { get; set; }
        public int QuantityAvailable { get; set; }
        public bool IsVip { get; set; }

    }
}
