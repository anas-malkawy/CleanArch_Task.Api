using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArch_Task.Domain.Entities
{
    public class Customer
    {
        public int Id { get; set; }  // Identity في DB
        public string FullName { get; set; } = null!;
        public string Email { get; set; } = null!;
    }

}
