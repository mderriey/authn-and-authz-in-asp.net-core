using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using AuthenticationAndAuthorisation.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;

namespace AuthenticationAndAuthorisation.Controllers
{
    public class HomeController : Controller
    {
        private readonly IAuthenticationSchemeProvider _schemeProvider;

        public HomeController(IAuthenticationSchemeProvider schemeProvider)
        {
            _schemeProvider = schemeProvider;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet("login")]
        public async Task<IActionResult> Login()
        {
            var authenticationSchemes = await _schemeProvider.GetAllSchemesAsync();
            var publicFacingSchemes = authenticationSchemes.Where(x => !string.IsNullOrEmpty(x.DisplayName));

            return View(publicFacingSchemes);
        }

        [HttpPost("login")]
        public async Task Login(string scheme)
        {
            await HttpContext.ChallengeAsync(scheme);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
