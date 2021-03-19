using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using audioplayer.Models;
using Microsoft.AspNetCore.Mvc;

namespace audioplayer.Controllers
{
    public class PlaylistController : Controller
    {
        PlaylistDAL playlistDAL = new PlaylistDAL();
        MusicDAL musicDAL = new MusicDAL();
        public IActionResult Index()
        {
            List<Playlist> playlists = new List<Playlist>();
            playlists = playlistDAL.GetPlaylists().ToList();
            return View(playlists);
        }

        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Add([Bind] Playlist playlist)
        {
            if(ModelState.IsValid)
            {
                playlistDAL.AddPlaylist(playlist);
                return RedirectToAction("Index");
            }
            return View(playlist);
        }

        public IActionResult Edit(int? Id)
        {
            if(Id == null)
            {
                return NotFound();
            }

            PlaylistMusic playlist = new PlaylistMusic();
            playlist.Playlist = playlistDAL.GetPlaylist(Id);

            if (playlist == null)
            {
                return NotFound();
            }

            playlist.Music = musicDAL.GetAllMusic();

            return View(playlist);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int? Id, [Bind] Playlist playlist)
        {
            if (Id == null)
            {
                return NotFound();
            }
            if (ModelState.IsValid)
            {
                playlistDAL.UpdatePlaylist(playlist);
                return RedirectToAction("Index");
            }
            return View(playlistDAL);
        }

        public IActionResult Details(int? Id)
        {
            if (Id == null)
            {
                return NotFound();
            }

            PlaylistMusic playlist = new PlaylistMusic();
            playlist.Playlist = playlistDAL.GetPlaylist(Id);

            if (playlist == null)
            {
                return NotFound();
            }

            List<Music> music = new List<Music>();
            string[] ids = playlist.Playlist.Titles.Split('-');

            foreach (var id in ids)
            {
                if (id != "")
                {
                    music.Add(musicDAL.GetMusic(int.Parse(id)));
                }
            }
            if (music != null)
            {
                playlist.Music = music;
            }

            return View(playlist);
        }

        public IActionResult Delete(int? Id)
        {
            if (Id == null)
            {
                return NotFound();
            }
            Playlist playlist = playlistDAL.GetPlaylist(Id);
            if (playlist == null)
            {
                return NotFound();
            }
            return View(playlist);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeletePlaylist(int? Id)
        {
            playlistDAL.DeletePlaylist(Id);
            return RedirectToAction("Index");
        }
    }
}
