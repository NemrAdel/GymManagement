using GymManagmentDAL.Models;
using GymManagmentDAL.Models.Enums;
using Microsoft.AspNetCore.Mvc;

namespace GymManagementPL.Controllers
{
    public class HomeController : Controller // specify for view returning , baseController for API
    {                                       // end with controller will not be Methods it will be Actions


        public ActionResult Index()
        {
            return View();
            //return NotFound();
            //return Redirect("https://www.google.com");
        }
        public ViewResult Loginn()
        {
            return View(); // will search for view with same name as action in folder with same name as controller
        }
        public JsonResult TestData()
        {
            var trainer=new Trainer()
            {
                Id = 1,
                Gender = Gender.Male,
                Phone = "01220818724",
                Name = "Ahmed" 
            };
            return Json(trainer);

        }
        public RedirectResult GotoGoogle()
        {
            return Redirect("https://www.google.com");
        }
        public ContentResult GetContent()
        {
            return Content("Hello from content result"); // , text/html
        }

        public FileResult DownloadFileResult()
        {
            var filePath = Path.Combine(Directory.GetCurrentDirectory(),"wwwroot/css/site.css");
            var fileBytes = System.IO.File.ReadAllBytes(filePath);  
            return File(fileBytes,"text/css","TestName|Style.css");
        }
    }
}
