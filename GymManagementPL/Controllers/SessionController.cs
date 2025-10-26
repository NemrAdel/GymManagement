using GymManagementBLL.BusinessServices.Implemintation;
using GymManagementBLL.BusinessServices.Interfaces;
using GymManagementSystemBLL.View_Models.SessionVm;
using GymManagmentDAL.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace GymManagementPL.Controllers
{
    public class SessionController : Controller
    {
        private readonly ISessionService _sessionService;
        private readonly ITrainerService _trainerService;
        private readonly ICategoryService _categoryService;

        public SessionController(ISessionService sessionService,ITrainerService trainerService,ICategoryService categoryService)
        {
            _sessionService = sessionService;
            _trainerService = trainerService;
            _categoryService = categoryService;
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
            LoadDropDowns();
            return View();
        }

        public ActionResult ConfirmCreate(CreateSessionViewModel createSession)
        {
                if (!ModelState.IsValid)
                {
                    LoadDropDowns();
                    ModelState.AddModelError("DataInvalid", "Please correct the errors and try again.");
                    return View("Create", createSession);
                }
                var isCreated = _sessionService.CreateSession(createSession);
                if (!isCreated)
                {
                    TempData["ErrorMessage"] = "Failed to create member.";
                    return View(nameof(Create), createSession);
                }
                LoadDropDowns();
                TempData["SuccessMessage"] = "Session created successfully.";
                return RedirectToAction(nameof(Index));
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
            return View(sessions);
        }
        public ActionResult Edit(int id)
        {
            if (id <= 0)
            {
                TempData["ErrorMessage"] = "Invalid Id Try Again";
                return RedirectToAction(nameof(Index));
            }
            var sessionToUpdate=_sessionService.GetSessionToUpdate(id);
            var GetAllTrainers = _trainerService.GetAllTrainers();
            if (sessionToUpdate is null)
            {
                TempData["ErrorMessage"] = "No Data To Show";
                return RedirectToAction(nameof(Index));
            }
            var trainerSelectList = GetAllTrainers.Select(t => new SelectListItem
            {
                Value = t.Id.ToString(),
                Text = t.Name
            });
            ViewBag.Trainers = trainerSelectList;
            return View(sessionToUpdate);
        }
        [HttpPost]
        public ActionResult Edit(int id,UpdateSessionViewModel updateSession)
        {
            if (id <= 0)
            {
                TempData["ErrorMessage"] = "Invalid Id Try Again";
                return RedirectToAction(nameof(Index));
            }
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("DataInvalid", "Please correct the errors and try again.");
                return View(updateSession);
            }
            var sessionUpdate=_sessionService.UpdateSession(id,updateSession);
            if (!sessionUpdate)
            {
                TempData["ErrorMessage"] = "Failed To Update Session";
                return RedirectToAction(nameof(Index));
            }
            TempData["SuccessMessage"] = "Session updated successfully.";
            return RedirectToAction(nameof(Index));
        }

        public ActionResult Delete(int id)
        {
            ViewBag.Id = id;
            return View();
        }
        [HttpPost]
        public ActionResult ConfirmDelete(int id)
        {
            if (id <= 0)
            {
                TempData["ErrorMessage"] = "Invalid Id Try Again";
                return RedirectToAction(nameof(Index));
            }
            var SessionDelete= _sessionService.DeleteSession(id);
            if (!SessionDelete)
            {
                TempData["ErrorMessage"] = "Failed To Delete Session";
                return RedirectToAction(nameof(Index));
            }
            TempData["SuccessMessage"] = "Session Deleted successfully.";
            return RedirectToAction(nameof(Index));
        }
        #region Helper Method
        private void LoadDropDowns()
        {
            var trainers = _sessionService.GetAllTrainersForDropDown();

            var categoris = _sessionService.GetAllCategoriesForDropDown();
            var sessionWithCategory = categoris.Select(s => new SelectListItem
            {
                Value = s.Id.ToString(),
                Text = s.CategoryName
            });
            var sessionWithTrainer = trainers.Select(s => new SelectListItem
            {
                Value = s.Id.ToString(),
                Text = s.Name
            });
            ViewBag.Category = sessionWithCategory;
            ViewBag.Trainer = sessionWithTrainer;
        }
        #endregion

    }
}
