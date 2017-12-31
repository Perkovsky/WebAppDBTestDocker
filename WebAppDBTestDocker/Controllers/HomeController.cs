using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using WebAppDBTestDocker.Models;

namespace WebAppDBTestDocker.Controllers
{
    public class HomeController : Controller
    {
        private IUserRepository userRepository;
        private IGuidRepository guidRepository;
        private string message;

        public HomeController(IUserRepository userRepository, IGuidRepository guidRepository, IConfiguration config)
        {
            this.userRepository = userRepository;
            this.guidRepository = guidRepository;
            message = $"Hostname: {config["HOSTNAME"]}";
        }

        public IActionResult Index()
        {
            ViewBag.Message = message;
            return View(new ViewModel(userRepository.Users, guidRepository.Guids));
        }

        [NonAction]
        private int GetRandomNumber(int max = 100)
        {
            var rand = new Random();
            return rand.Next((max == 100) ? 10 : 100, max);
        }

        [NonAction]
        private User GetNewUser()
        {
            User user = new Models.User
            {
                Name = Guid.NewGuid().ToString().Substring(0, 8),
                Age = GetRandomNumber(),
                Phone = $"({GetRandomNumber(1000)}) {GetRandomNumber(1000)}-{GetRandomNumber()}-{GetRandomNumber()}"
            };
            return user;
        }

        [HttpPost]
        public RedirectToActionResult AddRandomUser()
        {
            userRepository.SaveUser(GetNewUser());
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public RedirectToActionResult AddGuid()
        {
            guidRepository.SaveGuid(new MyGuid { Guid = Guid.NewGuid().ToString() });
            return RedirectToAction(nameof(Index));
        }
    }
}