using GymManagementBLL.BusinessServices.Interfaces;
using GymManagmentDAL.Models;
using GymManagmentDAL.Models.Enums;
using Microsoft.AspNetCore.Mvc;

namespace GymManagementPL.Controllers
{
    public class HomeController : Controller // specify for view returning , baseController for API
    {                                       // end with controller will not be Methods it will be Actions
        private readonly IAnalyticsService _analyticsService;

        public HomeController(IAnalyticsService analyticsService)
        {
            _analyticsService = analyticsService;
        }
        public ActionResult Index()
        {
            var data = _analyticsService.GetHomeAnalyticsService();
            //return View();
            return View(data); // will search for view with same name as action in folder with same name as controller
        }

        public ViewResult Privacy()
        {
            return View(); // will search for view with same name as action in folder with same name as controller
        }

    }
}
