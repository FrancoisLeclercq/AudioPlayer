using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using audioplayer.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace audioplayer.Controllers
{
    public class HomeController : Controller
    {
        MusicDAL musicDAL = new MusicDAL();

        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View(musicDAL.GetAllMusic().ToList());
        }

        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Add([Bind] Music music, List<IFormFile> FileData)
        {
            if (ModelState.IsValid)
            {
                foreach (var item in FileData)
                {
                    if (item.Length > 0)
                    {
                        using (var stream = new MemoryStream())
                        {
                            await item.CopyToAsync(stream);
                            music.FileData = stream.ToArray();
                        }
                        musicDAL.AddMusic(music);
                    }
                }
                return RedirectToAction("Index");
            }

            return View(music);
        }

        public IActionResult Delete(int? Id)
        {
            if (Id == null) return RedirectToAction("Index");

            Music music = musicDAL.GetMusic(Id);
            if (music == null)
            {
                return RedirectToAction("Index");
            }

            return View(music);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteSong(int? Id)
        {
            musicDAL.DeleteMusic(Id);
            return RedirectToAction("Index");
        }


        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
