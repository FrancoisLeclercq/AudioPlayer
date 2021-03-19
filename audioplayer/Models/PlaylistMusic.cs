using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace audioplayer.Models
{
    public class PlaylistMusic
    {
        public Playlist Playlist { get; set; }
        public List<Music> Music { get; set; }
    }
}
