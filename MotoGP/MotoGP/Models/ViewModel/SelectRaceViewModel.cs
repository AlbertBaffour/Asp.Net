using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MotoGP.Models.ViewModel
{
    public class SelectRacetViewModel
    {
        public SelectList Races { get; set; }

        public int raceID { get; set; }
        public Race selectedRace { get; set; }
    }
}
