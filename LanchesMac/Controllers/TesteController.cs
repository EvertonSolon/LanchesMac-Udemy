using Microsoft.AspNetCore.Mvc;

namespace LanchesMac.Controllers
{
    public class TesteController : Controller
    {
        public string Index()
        {
            return $"Teste index do Controller Teste {DateTime.Now}";
        }
    }
}
