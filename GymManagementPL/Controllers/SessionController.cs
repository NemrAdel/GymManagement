using GymManagementBLL.BusinessServices.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace GymManagementPL.Controllers
{
    public class SessionController : Controller
    {
        private readonly ISessionService _sessionService;

        public SessionController(ISessionService sessionService)
        {
            _sessionService = sessionService;
        }
        public IActionResult Index()
        {
            var sessions = _sessionService.GetAllSessions();
            if (sessions is null || !sessions.Any())
            {
                TempData["InfoMessage"] = "No sessions available.";
            }

            return View(sessions);
        }

        public ActionResult Create()
        {

            return View();
        }

        public ActionResult Details(int id)
        {
            if (id <= 0)
            {
                TempData["ErrorMessage"] = "Invalid Id Try Again";
                return RedirectToAction(nameof(Index));
            }
            var sessions = _sessionService.GetSessionsDetails(id);
            if(sessions is null)
            {
                TempData["ErrorMessage"] = "Session Not Found";
                return RedirectToAction(nameof(Index));
            }
            return View();
        }
    }
}
