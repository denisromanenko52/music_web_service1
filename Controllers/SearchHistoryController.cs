using Microsoft.AspNetCore.Mvc;
using music_web_service1.Models.DAL;
using music_web_service1.Models.Domain;

namespace music_web_service1.Controllers
{
    public class SearchHistoryController : Controller
    {
        private readonly DataService _dataService;

        public SearchHistoryController(DataService dataService)
        {
            _dataService = dataService;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> GetIndex(string email)
        {
            if (email != null)
            {
                List<SearchHistory> history = await _dataService.GetSearchHistory(email);
                return View("Index", history);
            }
            else
            {
                return RedirectToAction("Index");
            }
        }

        [HttpPost]
        public async Task<IActionResult> SearchHistory([FromBody] Track track)
        {
            var email = HttpContext.Session.GetString("Email");

            if (email != null)
            {
                await _dataService.AddSearchHistory(email, track.Name, track.Audio);
            }

            return Ok();
        }
    }
}
