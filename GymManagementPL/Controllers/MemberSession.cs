using Microsoft.AspNetCore.Mvc;

namespace GymManagementPL.Controllers
{
    public class MemberSession : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
