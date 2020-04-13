using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using WebLoggingSample.Models;

namespace WebLoggingSample.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        
        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Index(IndexAuthModel model)
        {
            return Redirect("Home/Comment");
        }

        public IActionResult Comment()
        {
            return View();
        }
        
        [HttpPost]
        public IActionResult Comment(string comment)
        {
            _logger.LogError($"Test exception on comment '{comment}'");
            return Ok($"Test exception on comment '{comment}'");
        }
    }
}