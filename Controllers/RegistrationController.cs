using Microsoft.AspNetCore.Mvc;
using music_web_service1.Models.DAL;
using music_web_service1.Models.Domain;
using music_web_service1.Models.RabbitMQ;

namespace music_web_service1.Controllers
{
    public class RegistrationController : Controller
    {
        private readonly DataService _dataService;
        private readonly RabbitMQService _rabbitMQService;

        public RegistrationController(DataService dataService, RabbitMQService rabbitMQService)
        {
            _dataService = dataService;
            _rabbitMQService = rabbitMQService;
        }

        public IActionResult Registration()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Registration(Account model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            else
            {
                bool succesAdd = await _dataService.AddUser(model.Email, model.Password);

                if (!succesAdd)
                {
                    ModelState.AddModelError(string.Empty, "Пользователь с таким Email уже существует");
                    ViewData["ValidationSummaryId"] = "authValidationSummary";
                    return View(model);
                }
                else
                {
                    _rabbitMQService.SendMessage("registration_queue", "Пользователь зарегистрировался!");
                    return RedirectToAction("Index", "Account");
                }
            }
        }
    }
}
