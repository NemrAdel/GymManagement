using GymManagementBLL.BusinessServices.Interfaces;
using GymManagementBLL.View_Models;
using Microsoft.AspNetCore.Mvc;

namespace GymManagementPL.Controllers
{
    public class MemberController : Controller
    {
        private readonly IMemberService _memberService;

        public MemberController(IMemberService memberService)
        {
            _memberService = memberService;
        }
        public ActionResult Index()
        {
            var members = _memberService.GetAllMembers();
            return View(members);
        }
        public ActionResult MemberDetails(int id)
        {
            if (id <= 0)
            {
                TempData["ErrorMessage"] = "Invalid Member Id.";
                return RedirectToAction(nameof(Index));
            }
            var member = _memberService.GetMemberDetails(id);
            if (member is null)
            {
                TempData["ErrorMessage"] = "Member not found.";
                return RedirectToAction(nameof(Index));
            }
            return View(member);
        }
        public ActionResult HealthRecordDetails(int id)
        {
            if (id <= 0)
                return RedirectToAction(nameof(Index));
            var healthRecord = _memberService.GetMemberHealthDetails(id);
            if (healthRecord is null)
                return RedirectToAction(nameof(Index));

            return View(healthRecord);
        }

        public ActionResult Create()
        {
            return View();
        }
        public ActionResult CreateMember(CreateMemberViewModel createMember)
        {
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("DataInvalid", "Please correct the errors and try again.");
                return View("Create", createMember);
            }
            var isCreated = _memberService.CreateMember(createMember);
            if (!isCreated)
            {
                TempData["ErrorMessage"] = "Failed to create member.";
                return View(nameof(Create), createMember);
            }
            TempData["SuccessMessage"] = "Member created successfully.";
            return RedirectToAction(nameof(Index));

        }
        public ActionResult MemberEdit(int id)
        {
            if (id <= 0)
            {
                TempData["ErrorMessage"] = "Id Cannot be negative or zero.";
                return RedirectToAction(nameof(Index));
            }
            var memberToUpdate = _memberService.GetMemberDetailsToUpdate(id);
            if (memberToUpdate is null)
            {
                TempData["ErrorMessage"] = "Id Is Null.";
                return RedirectToAction(nameof(Index));
            }
            return View(memberToUpdate);
        }
        [HttpPost]
        public ActionResult MemberEdit(int id, MemberToUpdateViewModel UpdateMember) //from form
        {
            if (id <= 0)
            {
                TempData["ErrorMessage"] = "Id Cannot be negative or zero.";
                return RedirectToAction(nameof(Index));
            }
            if (!ModelState.IsValid)
            {
                Console.WriteLine("Nooo");
                ModelState.AddModelError("DataInvalid", "Please correct the errors and try again.");
                return View(UpdateMember);
            }
            var isUpdated = _memberService.UpdateMember(id, UpdateMember);
            if (!isUpdated)
            {
                Console.WriteLine("no");
                TempData["ErrorMessage"] = "Failed to update member.";
                return View(UpdateMember);
            }
            TempData["SuccessMessage"] = "Member updated successfully.";
            return RedirectToAction(nameof(Index));
        }

        public ActionResult Delete(int id)
        {
            if (id <= 0)
            {
                TempData["ErrorMessage"] = "Id Cannot be negative or zero.";
                return RedirectToAction(nameof(Index));
            }
            var member = _memberService.GetMemberDetails(id);
            if (member is null)
            {
                TempData["ErrorMessage"] = "Member not found.";
                return RedirectToAction(nameof(Index));
            }
            ViewBag.MemberId = id;
            return View();



        }
        [HttpPost]
        public ActionResult DeleteConfirmed(int id)// from form
        {
            if (id <= 0)
            {
                TempData["ErrorMessage"] = "Id Cannot be negative or zero.";
                return RedirectToAction(nameof(Index));
            }
            var isDeleted = _memberService.RemoveMember(id);
            if (!isDeleted)
            {
                TempData["ErrorMessage"] = "Failed to delete member.";
                return RedirectToAction(nameof(Index));
            }
            TempData["SuccessMessage"] = "Member deleted successfully.";
            return RedirectToAction(nameof(Index));
        }

    }
}
