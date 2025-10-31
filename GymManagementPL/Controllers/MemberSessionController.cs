using GymManagementBLL.BusinessServices.Interfaces;
using GymManagementBLL.View_Models.MemberSessionviewModel;
using GymManagmentDAL.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

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
        [HttpGet]
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
                return RedirectToAction(nameof(OnGoing), new { id = id }  );
            }
            var result = _memberSessionService.Attendance(id);
            return RedirectToAction(nameof(OnGoing), new { id = id });
        }

        public ActionResult UpComing(int id)
        {
            var memberSession = _memberSessionService.GetMembersWithSessionId(id);
            if (memberSession == null)
            {
                TempData["ErrorMessage"] = "No upcoming sessions found.";
                return RedirectToAction(nameof(Index));
            }
            ViewBag.SessionId = id;
            Console.WriteLine(id);
            return View(memberSession);
        }

        public ActionResult Create(int id)
        {
            var members = _memberSessionService.GetMemberSessionWithMemberAndSession();
            ViewBag.Members = new SelectList(members, "MemberId", "Members");
            ViewBag.SessionId = id;
            return View();
        }
        [HttpPost]
        public ActionResult Create(CreateMemberSessionViewModel createMember)
        {
            if (createMember == null)
            {
                TempData["ErrorMessage"] = "Invalid session data.";
                return RedirectToAction(nameof(Create));
            }
            var isCreated = _memberSessionService.Create(createMember);
            if (!isCreated)
            {
                TempData["ErrorMessage"] = "Session Failed To Create.";
                return RedirectToAction(nameof(Create));
            }
            TempData["SuccessMessage"] = "Session created successfully.";
            return View(nameof(Index));
        }
    }
}
