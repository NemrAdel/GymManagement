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
        public ActionResult TrainerDetails(int id)
        {
            if(id <= 0)
            {
                TempData["ErrorMessage"] = "Invalid Trainer Id.";
                return RedirectToAction(nameof(Index));
            }
            var trainer= _trainerService.GetTrainerDetails(id);
            if(trainer is null){
                TempData["ErrorMessage"] = "Trainer not found.";
                return RedirectToAction(nameof(Index));
            }
            return View(trainer);
        }
        public ActionResult Create()
        {
            return View();
        }
    }
}

