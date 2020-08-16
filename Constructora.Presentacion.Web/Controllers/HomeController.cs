using Microsoft.AspNetCore.Mvc;

namespace Constructora.Presentacion.Web.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View("Index");
        }
    }
}
