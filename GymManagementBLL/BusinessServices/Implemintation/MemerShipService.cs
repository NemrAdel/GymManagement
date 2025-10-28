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
        public bool Create(CreateMemberShipViewModel membership)
        {
            if (membership is null)
                return false;

            var mappedMemberShip = _mapper.Map<MemberShip>(membership);
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
