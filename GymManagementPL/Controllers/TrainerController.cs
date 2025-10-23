using GymManagementBLL.BusinessServices.Implemintation;
using GymManagementBLL.BusinessServices.Interfaces;
using GymManagementBLL.View_Models;
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
        public ActionResult CreateTrainer(CreateTrainerViewModel createTrainer)
        {

            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("DataInvalid", "Please correct the errors and try again.");
                return View("Create", createTrainer);
            }
            var isCreated = _trainerService.CreateTrainer(createTrainer);
            if (!isCreated)
            {
                TempData["ErrorMessage"] = "Failed to create trainer. Please try again.";
                return View(nameof(Create),createTrainer);
            }
            TempData["SuccessMessage"]= "Trainer Created Successfully";
            return RedirectToAction(nameof(Index));
        }
        public ActionResult Delete(int id)
        {
            if (id <= 0)
            {
                TempData["ErrorMessage"] = "Invalid Trainer Id.";
                return RedirectToAction(nameof(Index));
            }
            var trainer = _trainerService.GetTrainerDetails(id);
            if (trainer is null)
            {
                TempData["ErrorMessage"] = "Trainer not found.";
                return RedirectToAction(nameof(Index));
            }
            ViewBag.TrainerId= id;
            return View(trainer);
        }


        public ActionResult ConfirmDelete(int id)
        {
            if (id <= 0)
            {
                TempData["ErrorMessage"] = "Invalid Trainer Id.";
                return RedirectToAction(nameof(Index));
            }
            var isDeleted = _trainerService.RemoveTrainer(id);
            if (!isDeleted)
            {

                TempData["ErrorMessage"] = "Failed to delete trainer. Please try again.";
                return RedirectToAction(nameof(Index));
            }
            TempData["SuccessMessage"] = "Trainer Deleted Successfully";
            return RedirectToAction(nameof(Index));
        }

        public ActionResult TrainerEdit(int id)
        {
            if (id <= 0)
            {
                TempData["ErrorMessage"] = "Invalid Trainer Id.";
                return RedirectToAction(nameof(Index));
            }
            var trainer = _trainerService.GetTrainerDetailsToUpdate(id);
            if (trainer is null)
            {
                TempData["ErrorMessage"] = "Trainer not found.";
                return RedirectToAction(nameof(Index));
            }
            return View(trainer);
        }
        [HttpPost] // to know that this method is called on form submission
        public ActionResult TrainerEdit(int id,TrainerToUpdateViewModel updateTrainer)
        {
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("DataInvalid", "Please correct the errors and try again.");
                return View(nameof(Index), updateTrainer);
            }
            var isUpdated = _trainerService.UpdateTrainer(id,updateTrainer);
            if (!isUpdated)
            {
                TempData["ErrorMessage"] = "Failed to update trainer. Please try again.";
                return View(nameof(Index), updateTrainer);
            }
            TempData["SuccessMessage"] = "Trainer Updated Successfully";
            return RedirectToAction(nameof(Index));
        }
    }
}

