using Microsoft.AspNetCore.Mvc;
using WebAppDBTestDocker.Models;

namespace WebAppDBTestDocker.Controllers
{
    public class HomeController : Controller
    {
        private IUserRepository repository;

        public HomeController(IUserRepository repository) => this.repository = repository;

        public IActionResult Index() => View(repository.Users);
    }
}