using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebApp.Controllers
{
    public class EventController : Controller
    {
        // GET: EventController
        public ActionResult Index()
        {
            return View();
        }
    }
}
