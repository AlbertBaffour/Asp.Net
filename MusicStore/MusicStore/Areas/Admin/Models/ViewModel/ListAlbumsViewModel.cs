using Microsoft.AspNetCore.Mvc.Rendering;
using MusicStore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MusicStore.Areas.Admin.Models.ViewModel
{
    public class ListAlbumsViewModel
    {
        public SelectList Genres { get; set; }
        public SelectList Artists { get; set; }
        public List<Album> Albums { get; set; }

        public int genreID { get; set; }
        public int artistID { get; set; }
        public string title { get; set; }
    }
}
