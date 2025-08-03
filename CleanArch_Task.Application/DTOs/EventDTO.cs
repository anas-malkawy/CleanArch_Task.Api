using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArch_Task.Application.DTOs
{
    public class EventDTO
    {
        public string Title { get; set; } = null!;
        public string Description { get; set; }
        public int VenueId { get; set; }
    }
}
