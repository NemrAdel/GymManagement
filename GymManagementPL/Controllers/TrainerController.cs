using Microsoft.AspNetCore.Mvc;

namespace GymManagementPL.Controllers
{
    public class TrainerController : Controller
    {
        public ActionResult Index(int id)
        {
            Console.WriteLine(id);
            return View();
        }
        public ActionResult GetTrainers()
        {
            return View();
        }
        public ActionResult Create()
        {
            return View();
        }
    }
}
