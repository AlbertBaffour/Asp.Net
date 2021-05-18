using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MotoGP.Models.ViewModel
{
    public class OrderTicketViewModel
    {
        public SelectList Landen { get; set; }
        public IEnumerable<Race> Races { get; set; }
        public Ticket ticket { get; set; }
    }
}
