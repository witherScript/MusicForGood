using Microsoft.AspNetCore.Mvc;

namespace MusicForGood.Controllers
{
    public class HomeController : Controller
    {

      [HttpGet("/")]
      public ActionResult Index()
      {
        return View();
      }

    }
}