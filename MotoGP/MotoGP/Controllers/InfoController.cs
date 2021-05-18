using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using MotoGP.Data;
using MotoGP.Models;
using MotoGP.Models.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MotoGP.Controllers
{
    public class InfoController : Controller
    {
        private readonly GPContext _context;

        public InfoController(GPContext context)
        {
            _context = context;
        }
        public IActionResult ListRaces()
        {
            var races = _context.Races;
            ViewData["BannerNr"] = 0;
            return View(races.ToList());
        }   
        public IActionResult ListRiders()
        {
            var riders = _context.Riders;
            ViewData["BannerNr"] = 1;
            return View(riders.ToList());
        }   
        public IActionResult ListTeams()
        {
            var teams = _context.Teams;
            ViewData["BannerNr"] = 2;
            return View(teams.ToList());
        }     
        public IActionResult BuildMap()
        {
            var races = _context.Races;
            ViewData["BannerNr"] = 0;
            return View(races.ToList());
        }     
        public IActionResult SelectRace(int raceID = 0)
        {
            var selectraceVM = new SelectRacetViewModel();
            selectraceVM.Races = new SelectList(_context.Races.OrderBy(m =>m.Name),
                "RaceID",
                "Name");
            if (raceID != 0)
            {
                selectraceVM.selectedRace = _context.Races.SingleOrDefault(m => m.RaceID == raceID);
            }
            else
            {
                selectraceVM.selectedRace = new Race();
            }
            ViewData["BannerNr"] = 0;
            return View(selectraceVM);
        }
        public IActionResult ShowRace(int id)
        {
            var race = _context.Races
                        .SingleOrDefault(m => m.RaceID == id);

            ViewData["BannerNr"] = 0;
            return View(race);
        }
    }
}
