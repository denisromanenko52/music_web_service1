using Microsoft.AspNetCore.Mvc;
using music_web_service1.Models.DAL;
using music_web_service1.Models.Domain;

namespace music_web_service1.Controllers
{
    public class AccountController : Controller
    {
        private readonly DataService _dataService;

        public AccountController(DataService dataService)
        {
            _dataService = dataService;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Index(string Email, string Password, Account model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            else
            {
                bool userExists = await _dataService.ExistUser(model.Email, model.Password);

                if (!userExists)
                {
                    ModelState.AddModelError(string.Empty, "Неверные учетные данные");
                    ViewData["ValidationSummaryId"] = "authValidationSummary";
                    return View(model);
                }
                else
                {
                    HttpContext.Session.SetString("Email", model.Email);
                    List<SearchHistory> history = await _dataService.GetSearchHistory(model.Email);

                    if (history != null && history.Count > 0)
                    {
                        return RedirectToAction("GetIndex", "SearchHistory", new { email = model.Email });
                    }
                    else
                    {
                        return RedirectToAction("Index", "Music");
                    }
                }
            }
        }

        public IActionResult Logout()
        {
            HttpContext.Session.Remove("Email");
            return RedirectToAction("Index", "Account");
        }
    }
}
