using GymManagementBLL.BusinessServices.Interfaces;
using Microsoft.AspNetCore.Mvc;

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
    }
}
