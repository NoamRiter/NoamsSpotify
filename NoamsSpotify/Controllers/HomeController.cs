using NoamsSpotify.Models;
using NoamsSpotify.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;


namespace NoamsSpotify.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<JsonResult> artistsName(string name)
        {
            SpotifyApiClient client = new SpotifyApiClient();
            var artistsName = await client.SearchArtistsNameAsync(name);

            return Json(artistsName);
        }

        [HttpPost]
        public async Task<JsonResult> getGenrs(List<string> name)
        {
            SpotifyApiClient client = new SpotifyApiClient();
            var artistList = await client.GetArtistsGangers(name);
            
            return Json(artistList);
        }

        [HttpPost]
        public async Task<JsonResult> getRelatedArtists(List<string> name)
        {
            SpotifyApiClient client = new SpotifyApiClient();
            var relatedArtists = await client.GetRelatedArtists(name);

            return Json(relatedArtists);
        }
  
        [HttpPost]
        public async Task<ActionResult> Result(List<string> genrs, List<string> relatedArtists, string popularityInputName, string limitInputName)
        {
            SpotifyApiClient client = new SpotifyApiClient();
            var rootObject = await client.GetResult(genrs, relatedArtists, popularityInputName, limitInputName);
            ResultViewModel model = new ResultViewModel();
            model.Tracks = rootObject.Tracks;
            double y = TimeSpan.FromMilliseconds(352293).TotalMinutes;
            return View(model);
        }
    }
}