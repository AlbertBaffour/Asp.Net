using Microsoft.AspNetCore.Mvc;
using MusicStore.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MusicStore.Components
{
    [ViewComponent(Name ="GenreMenu")]
    public class GenreMenuComponent: ViewComponent
    {
        private readonly StoreContext _context;

        public GenreMenuComponent(StoreContext context)
        {
            _context = context;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            var genres = _context.Genres.OrderBy(g => g.Name).Take(8);
            return View(genres.ToList());
        }
    }
}
