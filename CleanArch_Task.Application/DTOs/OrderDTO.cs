using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArch_Task.Application.DTOs
{
    public class OrderDTO
    {
        public int EventId { get; set; }
        public int CustomerId { get; set; }
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
    }
}
