using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LanchesMac.Controllers
{
    [Authorize]
    //[Authorize(Roles ="Admin")] <- outra forma de implementar a autorização com definição de roles.
    public class ContatoController : Controller
    {
        public IActionResult Index()
        {
            //if (User.Identity.IsAuthenticated) { return View(); }

            //return RedirectToAction("Login", "Account");
            return View();
        }
    }
}
