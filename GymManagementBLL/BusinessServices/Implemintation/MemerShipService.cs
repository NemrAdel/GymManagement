using AutoMapper;
using GymManagementBLL.BusinessServices.Interfaces;
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

        public MemerShipService(IUnitOfWork unitOfWork,IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;

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
