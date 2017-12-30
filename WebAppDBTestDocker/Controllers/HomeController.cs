using System;
using Microsoft.AspNetCore.Mvc;
using WebAppDBTestDocker.Models;

namespace WebAppDBTestDocker.Controllers
{
    public class HomeController : Controller
    {
        private IUserRepository repository;

        public HomeController(IUserRepository repository) => this.repository = repository;

        public IActionResult Index() => View(repository.Users);

        private int GetRandomNumber(int max = 100)
        {
            var rand = new Random();
            return rand.Next((max == 100) ? 10 : 100, max);
        }

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
            repository.SaveUser(GetNewUser());
            return RedirectToAction(nameof(Index));
        }
    }
}