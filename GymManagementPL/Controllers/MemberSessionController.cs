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
            var members = _memberSessionService.GetMemberSessionWithMemberAndSession().Distinct();
            var SelectListMembers=members.Select(m => new SelectListItem
            {
                Value = m.MemberId.ToString(),
                Text = m.Members.Name
            }).ToList().Distinct();
            ViewBag.Members = new SelectList(SelectListMembers,"Value","Text");
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
            if (!ModelState.IsValid)
            {
                TempData["ErrorMessage"] = "Please correct the form errors.";
                return View(createMember);
            }
            var memberExists = _memberSessionService.GetMemberSessionWithMemberAndSession()
                .Any(ms => ms.MemberId == createMember.MemberId && ms.SessionId==createMember.SessionId);
            if (memberExists)
            {
                TempData["ErrorMessage"] = "Member is already enrolled in this session.";
                return View(createMember);
            }
                var isCreated = _memberSessionService.Create(createMember);
            if (!isCreated)
            {
                TempData["ErrorMessage"] = "Session Failed To Create.";
                return View(createMember);
            }
            TempData["SuccessMessage"] = "Session created successfully.";
            return RedirectToAction(nameof(UpComing),new { id=createMember.SessionId});
        }

        public ActionResult Cancel(int id)
        {
            var memberSessions = _memberSessionService.GetMemberSessionWithMemberAndSession();
            var memberSession = memberSessions.FirstOrDefault(ms => ms.MemberId == id);
            if (memberSession == null)
            {
                TempData["ErrorMessage"] = "Member session not found.";
                return RedirectToAction(nameof(UpComing),new {id=memberSession.SessionId});
            }
            var cancelMemberSession=_memberSessionService.Cancel(memberSession.Id);
            return RedirectToAction(nameof(UpComing));
        }
    }
}
