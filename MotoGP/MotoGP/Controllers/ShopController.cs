using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MotoGP.Data;
using MotoGP.Models;
using MotoGP.Models.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MotoGP.Controllers
{
    public class ShopController : Controller
    {
        private readonly GPContext _context;

        public ShopController(GPContext context)
        {
            _context = context;
        }
        public IActionResult OrderTicket()
        {
            var orderticketVM = new OrderTicketViewModel();
            orderticketVM.Landen = new SelectList(_context.Countries.OrderBy(m =>m.Name),
                "CountryID",
                "Name");
            orderticketVM.Races =(_context.Races.OrderBy(m =>m.Name).ToList());

            ViewData["BannerNr"] = 3;
            return View(orderticketVM);
        }
        public IActionResult ConfirmOrder(int id)
        {
            var ticket = _context.Tickets
                .Include(m=>m.Race)
                .SingleOrDefault(r => r.TicketID == id);

            ViewData["BannerNr"] = 3;
            return View(ticket);
        }
        // POST: Order/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("TicketID,Name,Email,Address,Number,CountryID,RaceID")] Ticket ticket)
        {
            
            ticket.OrderDate = DateTime.Now;
            ticket.Paid = false;
            if (ModelState.IsValid)
            {
                _context.Add(ticket);
                _context.SaveChanges();
                return RedirectToAction("ConfirmOrder",new { id = ticket.TicketID });
            }
            
            return View("OrderTicket");
        }
        public IActionResult ListTickets(int raceID = 0)
        {
            var listticketsVM = new ListTicketsViewModel();
           listticketsVM.Races = new SelectList(_context.Races.OrderBy(m => m.Name),
                "RaceID",
                "Name");
            if (raceID != 0)
            {
                listticketsVM.Tickets = _context.Tickets.Where(m => m.RaceID == raceID).Include(m=>m.Country).OrderBy(m => m.OrderDate).ToList();
            }
            else
            {
                listticketsVM.Tickets = _context.Tickets.Include(m => m.Country).OrderBy(m => m.OrderDate).ToList();

            }
            ViewData["BannerNr"] = 3;
            return View(listticketsVM);
        }
        // GET: Tickets/Edit/5
        public IActionResult EditTicket(int id)
        {
            var ticket= _context.Tickets.Include(m => m.Country).Include(m => m.Race).SingleOrDefault(r => r.TicketID == id);

            ViewData["BannerNr"] = 3;
            return View(ticket);
        }

        // POST: Ticketss/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult EditTicket(int id, [Bind("Paid")] Ticket ticket)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    if (ticket.Paid)
                    {
                        ticket.Paid = true;
                    }
                    else
                    {
                        ticket.Paid = false;
                    }   
                    _context.Update(ticket);
                    _context.SaveChanges();
                }
                catch (DbUpdateConcurrencyException)
                {
                    throw;
                }
                return RedirectToAction("ListTickets");
            }

            ViewData["BannerNr"] = 3;
            return View("ListTickets");
        }
    }
}
