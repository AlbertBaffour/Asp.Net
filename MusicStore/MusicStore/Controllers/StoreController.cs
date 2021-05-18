using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using MusicStore.Data;
using MusicStore.Models;
using MusicStore.Models.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MusicStore.Controllers
{
    public class StoreController : Controller
    {
        private readonly StoreContext _context;

        private readonly ILogger<StoreController> _logger;

        public StoreController(ILogger<StoreController> logger, StoreContext context)
        {
            _logger = logger;
            _context = context;
        }
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult ListGenres()
        {
            var genres = _context.Genres.OrderBy(g=>g.Name);
            return View(genres.ToList());
        }
        public IActionResult ListAlbums(int id)
        {
            var listalbumsVM = new ListAlbumsViewModel();
            listalbumsVM.genre = _context.Genres.FirstOrDefault(g => g.GenreID == id);
            listalbumsVM.Albums = (_context.Albums
                .Where(a=>a.GenreID==id)
                .OrderBy(g => g.Title)).ToList();
            return View(listalbumsVM);
        }
        public IActionResult Details(int id)
        {
            var album = _context.Albums
                .Include(a => a.Artist)
                .Include(a => a.Genre)
                .SingleOrDefault(a=> a.AlbumID==id);
            return View(album);
        }
     
    }
}
