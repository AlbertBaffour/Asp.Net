using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MusicStore.Models.ViewModel
{
    public class ListAlbumsViewModel
    {
        public Genre genre { get; set; }
        public List<Album> Albums { get; set; }
    }
}
