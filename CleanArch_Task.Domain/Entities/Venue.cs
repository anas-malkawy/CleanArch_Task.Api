using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArch_Task.Domain.Entities
{
    public class Venue
    {
        public int Id { get; set; }  // Identity في DB
        public string Name { get; set; } = null!;
        public string Location { get; set; } = null!;
        public int TenantId { get; set; }  
    }

}
