using Microsoft.AspNetCore.Mvc;

namespace LanchesMac.Controllers
{
    public class AdminController : Controller
    {
        public string Index()
        {
            return $"Teste Action Index do Controller Admin {DateTime.Now}";
        }

        public string Demo()
        {
            return $"Teste Action Demo do Controller Admin {DateTime.Now}";
        }
    }
}
