using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MusicStore.Data;
using MusicStore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MusicStore.Controllers
{
    public class ShoppingCartController : Controller
    {
        private readonly StoreContext _context;

        public ShoppingCartController( StoreContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            var sc = new ShoppingCart(HttpContext, _context);
            return View(sc.GetCartItems());
        }
        public IActionResult AddToCart(int id)
        {
            var sc = new ShoppingCart(HttpContext, _context);
            var album = _context.Albums
               .SingleOrDefault(a => a.AlbumID == id);
            sc.AddToCart(album);
            return RedirectToAction("Index");
        }
        public IActionResult IncreaseItem(int id)
        {
            var sc = new ShoppingCart(HttpContext, _context);
            sc.IncreaseItem(id);
            return RedirectToAction("Index");
        }
        public IActionResult DecreaseItem(int id)
        {
            var sc = new ShoppingCart(HttpContext, _context);
            sc.DecreaseItem(id);
            return RedirectToAction("Index");
        }
        public IActionResult RemoveFromCart(int id)
        {
           var sc = new ShoppingCart(HttpContext, _context);
            sc.RemoveFromCart(id);
            return RedirectToAction("Index");
        }
    }
}
