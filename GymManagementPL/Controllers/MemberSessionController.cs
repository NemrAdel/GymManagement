using GymManagementBLL.BusinessServices.Interfaces;
using GymManagmentDAL.Models;
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
                TempData["ErrorMessage"] = "Invalid member ID .";
                return RedirectToAction(nameof(Index));
            }
            var sessions = _memberSessionService.GetOnGoingSessionsBySessionId(id);
            Console.WriteLine($"Count is {sessions.Count()}");
            return View(sessions);
        }

        [HttpPost]
        public ActionResult OnGoing(int id,MemberSessions updateAttendance)
        {
            if (id <= 0)
            {
                TempData["ErrorMessage"] = "Invalid session ID.";
                return RedirectToAction(nameof(OnGoing));
            }
            var result = _memberSessionService.Attendance(id);
            return RedirectToAction(nameof(OnGoing));
        }
    }
}
