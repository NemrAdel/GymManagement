using AutoMapper;
using GymManagementBLL.BusinessServices.Interfaces;
using GymManagementBLL.EmailService;
using GymManagementBLL.View_Models.EmailViewModel;
using GymManagementBLL.View_Models.MemberShipVM;
using GymManagementDAL.Repositories.Implemintation;
using GymManagementDAL.Repositories.Interfaces;
using GymManagementDAL.UnitOfWork;
using GymManagmentDAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManagementBLL.BusinessServices.Implemintation
{
    
    public class MemerShipService : IMemberShip
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IEmailService _emailService;
        private readonly IMemberService _memberService;
        private readonly IPlanService _planService;

        public MemerShipService(IUnitOfWork unitOfWork,
            IMapper mapper,
            IEmailService emailService,
            IMemberService memberService,
            IPlanService planService)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _emailService = emailService;
            _memberService = memberService;
            _planService = planService;
        }

        public bool Cancel(int id)
        {
            try
            {
                if (id <= 0)
                    return false;
                var membership = _unitOfWork.MemberShipRepository.GetMemberShipByid(id);
                if (membership is null)
                    return false;
                _unitOfWork.GetRepository<MemberShip>().Delete(membership);
                var isCancelled = _unitOfWork.SaveChanges() > 0;
                if (!isCancelled)
                    return false;
                return isCancelled;
            }
            catch (Exception err)
            {
                Console.WriteLine(err);
                return false;
            }
        }

        public bool Create(CreateMemberShipViewModel membership)
        {
            var membershipExists = _unitOfWork.MemberShipRepository.GetAllWithMemberAndPlan();
            if(membershipExists.Any(x=>x.MemberId==membership.MemberId))
                return false;
            if (membership is null)
                return false;
            var planid = membership.PlanId;
            int Days = planid switch
            {
                1 => 30,
                2 => 50,
                3 => 90,
                4 => 365,
                _=>0
            };
            var mappedMemberShip = _mapper.Map<MemberShip>(membership);
            mappedMemberShip.EndDate = DateTime.Now.AddDays(Days);
            _unitOfWork.GetRepository<MemberShip>().Add(mappedMemberShip);
            var isCreated = _unitOfWork.SaveChanges() > 0;
            if (!isCreated)
                return false;


            var member = _memberService.GetMemberDetails(membership.MemberId);
            var plan= _planService.GetPlanDetails(membership.PlanId);
            _emailService.SendEmail(new EmailVM
            {
                To = member!.Email,
                Subject = "Membership Created Successfully ⚡",
                Body = $"Dear {member.Name},\n\n" +
                       $"Your membership has been created successfully! ✅\n\n" +
                       $"Membership Details:\n" +
                       $"- Plan Price: {plan!.Price}\n" +
                       $"- Duration Days: {plan.DurationDays}\n" +
                       $"- End Date: {mappedMemberShip.EndDate.ToShortDateString()}\n\n" +
                       $"Thank you for choosing our gym! ❤️⚡\n\n" +
                       $"Best regards,\n" +
                       $"Gym Management Team ⚡💪"
            });
            return isCreated;
        }

        public IEnumerable<MemberShipViewModel> GetAllActiveMemberShip()
        {
            var memberships =_unitOfWork.MemberShipRepository.GetAllWithMemberAndPlan();
            if (memberships is null || !memberships.Any())
                return [];

            var activeMemberships = memberships.Where(x => x.Status == "Active").ToList();
            return _mapper.Map<IEnumerable<MemberShipViewModel>>(activeMemberships);

        }
        public IEnumerable<Member> GetMemberForDropDown()
        {
            var members = _unitOfWork.GetRepository<Member>().GetAll();
            return _mapper.Map<IEnumerable<Member>>(members);
        }
        public IEnumerable<Plan> GetPlanForDropDown()
        {
            var plans = _unitOfWork.GetRepository<Plan>().GetAll();
            return _mapper.Map<IEnumerable<Plan>>(plans);
        }
    }
}
