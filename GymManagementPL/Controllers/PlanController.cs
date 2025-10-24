using GymManagementBLL.BusinessServices.Interfaces;
using GymManagementBLL.View_Models;
using Microsoft.AspNetCore.Mvc;

namespace GymManagementPL.Controllers
{
    public class PlanController : Controller
    {
        private readonly IPlanService _planService;

        public PlanController(IPlanService planService)
        {
            _planService = planService;
        }
        public IActionResult Index()
        {
            var plans = _planService.GetAllPlans();
            return View(plans);
        }

        public ActionResult PlanDetails(int id)
        {
            if (id <= 0)
            {
                TempData["ErrorMessage"] = "Invalid plan ID.";
                return RedirectToAction(nameof(Index));
            }
            var plan = _planService.GetPlanDetails(id);
            if (plan is null)
            {
                TempData["ErrorMessage"] = "Plan not found.";
                return RedirectToAction(nameof(Index));
            }
            return View(plan);
        }

        public ActionResult PlanEdit(int id) { 
            if (id <= 0)
            {
                TempData["ErrorMessage"] = "Invalid plan ID.";
                return RedirectToAction(nameof(Index));
            }
            var planToUpdate = _planService.GetPlanToUpdate(id);
            if (planToUpdate is null)
            {
                TempData["ErrorMessage"] = "Plan not found or cannot be updated.";
                return RedirectToAction(nameof(Index));
            }
            return View(planToUpdate);
        }
        [HttpPost]
        public ActionResult PlanEdit(int id,PlanToUpdateViewModel updatePlan) { 
            if (id <= 0)
            {
                TempData["ErrorMessage"] = "Invalid plan ID.";
                return RedirectToAction(nameof(Index));
            }
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("DataInvalid", "Please correct the errors and try again.");
                return View(updatePlan);
            }
            var planToUpdate = _planService.UpdatePlan(id,updatePlan);
            if (!planToUpdate)
            {
                TempData["ErrorMessage"] = "Failed to update plan. Please try again.";
                return View(updatePlan);
            }
            TempData["SuccessMessage"] = "Plan updated successfully.";
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public ActionResult Activate(int id)
        {
            if (id <= 0)
            {
                TempData["ErrorMessage"] = "Invalid plan ID.";
                return RedirectToAction(nameof(Index));
            }
            var activated = _planService.ToglleStatus(id);
            if (!activated)
            {
                TempData["ErrorMessage"] = "Failed to change plan status. Please try again.";
                return RedirectToAction(nameof(Index));
            }
            TempData["SuccessMessage"] = "Plan status changed successfully.";
            return RedirectToAction(nameof(Index));
        }
    }
}
