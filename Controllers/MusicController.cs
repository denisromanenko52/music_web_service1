using Microsoft.AspNetCore.Mvc;

namespace music_web_service1.Controllers
{
    public class MusicController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
