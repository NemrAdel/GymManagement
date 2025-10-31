using GymManagementBLL.BusinessServices.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace GymManagementPL.Controllers
{
    public class MemberSessionController : Controller
    {
        private readonly IMemberSessionService _memberSessionService;

        public MemberSessionController(IMemberSessionService memberSessionService)
        {
            _memberSessionService = memberSessionService;
        }
        public IActionResult Index()
        {
            var sessions =_memberSessionService.GetAllMemberSession();
            return View(sessions);
        }

        public ActionResult OnGoing(int id)
        {
            if (id <= 0)
            {
                TempData["ErrorMessage"] = "Invalid member ID.";
                return RedirectToAction(nameof(Index));
            }
            var sessions = _memberSessionService.GetOnGoingSessionsBySessionId(id);
            Console.WriteLine($"Count is {sessions.Count()}");
            return View(sessions);
        }
    }
}
