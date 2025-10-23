using GymManagementBLL.BusinessServices.Implemintation;
using GymManagementBLL.BusinessServices.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace GymManagementPL.Controllers
{
    public class TrainerController : Controller
    {
        private readonly ITrainerService _trainerService;

        public TrainerController(ITrainerService trainerService)
        {
            _trainerService = trainerService;
        }
        public ActionResult Index()
        {
            var trainers=_trainerService.GetAllTrainers();
            //if(trainers is null)
            //{
            //    Console.WriteLine("No Trainers Found");
            //}
            return View(trainers);
        }
        public ActionResult GetTrainers()
        {
            return View();
        }
        public ActionResult Create()
        {
            return View();
        }
    }
}

