using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArch_Task.Domain.Entities
{

    public class Event
    {
        public int Id { get; set; }  // Identity في DB
        public string Title { get; set; } = null!;
        public string Description { get; set; }
        public int VenueId { get; set; }  
    }


}
