using GymManagementBLL.BusinessServices.Interfaces;
using GymManagementBLL.View_Models.MemberShipVM;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace GymManagementPL.Controllers
{
    public class MemberShipController : Controller
    {
        private readonly IMemberShip _memberShip;

        public MemberShipController(IMemberShip memberShip)
        {
            _memberShip = memberShip;
        }
        public IActionResult Index()
        {
            var memberships = _memberShip.GetAllActiveMemberShip();
            if (memberships is null || !memberships.Any())
            {
                TempData["ErrorMessage"] = "No active memberships found.";
                return View();
            }
            
            return View(memberships);
        }
        public ActionResult Create()
        {
            LoadDropDown();
            return View();
        }
        public ActionResult ConfirmCreate(CreateMemberShipViewModel createMemberShip)
        {
            if (!ModelState.IsValid)
            {
                LoadDropDown();
                TempData["ErrorMessage"] = "Failed to create membership.";
                return View("Create", createMemberShip);
            }
            var isCreated = _memberShip.Create(createMemberShip);
            if (!isCreated)
            {
                TempData["ErrorMessage"] = "Failed to create membership.";
                return View(nameof(Create), createMemberShip);
            }
            LoadDropDown();
            TempData["SuccessMessage"] = "Membership created successfully.";
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Cancel(int id)
        {
            if (id <= 0)
            {
                TempData["ErrorMessage"] = "Invalid Membership Id.";
                return RedirectToAction(nameof(Index));
            }
            var isCancelled =_memberShip.Cancel(id);
            if (!isCancelled)
            {
                TempData["ErrorMessage"] = "Failed to cancel membership.";
                return RedirectToAction(nameof(Index));
            }
            TempData["SuccessMessage"] = "Membership cancelled successfully.";
            return RedirectToAction(nameof(Index));
        }

        #region Helper Method
        private void LoadDropDown()
        {
            var members = _memberShip.GetMemberForDropDown();
            var plans = _memberShip.GetPlanForDropDown();
            ViewBag.Members = new SelectList(members,"Id","Name");
            ViewBag.Plans = new SelectList(plans,"Id","Name");
        }
        #endregion
    }
}
