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
        public bool Create(MemberShipViewModel membership)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<MemberShipViewModel> GetAllActiveMemberShip()
        {
            var memberships =_unitOfWork.
            if (memberships is null || !memberships.Any())
                return [];

            var activeMemberships = memberships.Where(x => x.Status == "Active").ToList();
            return _mapper.Map<IEnumerable<MemberShipViewModel>>(activeMemberships);

        }
    }
}
