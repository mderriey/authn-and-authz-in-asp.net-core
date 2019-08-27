using System.Diagnostics;
using AuthenticationAndAuthorisation.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AuthenticationAndAuthorisation.Controllers
{
    public class HomeController : Controller
    {
        [Authorize(Policy = "Admin")]
        public IActionResult Index()
        {
            return View();
        }

        [Authorize(Policy = "Manager")]
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
