using AutoMapper;
using GymManagementBLL.BusinessServices.Interfaces;
using GymManagementBLL.View_Models;
using GymManagementDAL.UnitOfWork;
using GymManagmentDAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManagementBLL.BusinessServices.Implemintation
{
    public class PlanService : IPlanService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public PlanService(IUnitOfWork unitOfWork,IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public IEnumerable<PlanViewModel> GetAllPlans()
        {
            var Plans=_unitOfWork.GetRepository<Plan>().GetAll();
            if (Plans is null || !Plans.Any()) return [];
            #region Manual Mapping
            //return Plans.Select(p => new PlanViewModel
            //{
            //    Id = p.Id,
            //    Name = p.Name,
            //    Price = p.Price,
            //    DurationDays = p.DurationDays,
            //    IsActive = p.IsActive
            //}); 
            #endregion

            #region Auto Mapping
            return _mapper.Map<IEnumerable<Plan>,IEnumerable<PlanViewModel>>(Plans);
            #endregion
        }

        public PlanViewModel? GetPlanDetails(int PlanId)
        {
            var plan = _unitOfWork.GetRepository<Plan>().GetById(PlanId);
            if (plan is null) return null;
            #region Manual Mapping
            //return new PlanViewModel
            //{
            //    Id = plan.Id,
            //    Name = plan.Name,
            //    Description = plan.Description,
            //    DurationDays = plan.DurationDays,
            //    Price = plan.Price,
            //    IsActive = plan.IsActive
            //}; 
            #endregion

            // Auto Mapping
            return _mapper.Map<Plan,PlanViewModel>(plan);
        }

        public PlanToUpdateViewModel? GetPlanToUpdate(int planId)
        {
            var plan = _unitOfWork.GetRepository<Plan>().GetById(planId);
            if (plan is null || plan.IsActive ||HasActiveMemberShips(planId)) return null;

            #region Manual Mapping
            return new PlanToUpdateViewModel
            {
                Name = plan.Name,
                Description = plan.Description,
                DuratonDays = plan.DurationDays,
                price = plan.Price
            };
            #endregion

            // Auto Mapping
            //return _mapper.Map<Plan,PlanToUpdateViewModel>(plan);
        }

        public bool UpdatePlan(int planId, PlanToUpdateViewModel planToUpdate)
        {
            var planrep = _unitOfWork.GetRepository<Plan>(); // can used as simple way all time
            var plan = _unitOfWork.GetRepository<Plan>().GetById(planId);

            if(plan is null || planToUpdate is null) return false;


            #region Manual Mapping (Tuple)
            ////plan.Description = planToUpdate.Description;
            ////plan.DurationDays = planToUpdate.DuratonDays;
            ////plan.Price = planToUpdate.price;

            //(plan.Description, plan.DurationDays, plan.Price) =
            //    (planToUpdate.Description, planToUpdate.DuratonDays, planToUpdate.price);
            //plan.UpdatedAt = DateTime.Now;
            #endregion

            try
            {

                // Auto Mapping
                var planupdate =_mapper.Map<Plan>(planToUpdate); // as object to object mapping => same values form source to destination
                _unitOfWork.GetRepository<Plan>().Update(planupdate);
                plan.UpdatedAt = DateTime.Now;
                return _unitOfWork.SaveChanges() > 0;
            }
            catch (Exception)
            {

                return false;
            }
        }


        public bool ToglleStatus(int planId)
        {
            var plan = _unitOfWork.GetRepository<Plan>().GetById(planId);
            if (plan is null||HasActiveMemberShips(planId)) return false;
            plan.IsActive = plan.IsActive==true?false:true;

            plan.UpdatedAt = DateTime.Now;

            try
            {
                _unitOfWork.GetRepository<Plan>().Update(plan);
                return _unitOfWork.SaveChanges() > 0;
            }
            catch (Exception)
            {
                return false;
            }
        }


        #region HelperMethod
        private bool HasActiveMemberShips(int planId)
        {
            var hasActiveMemberShips = _unitOfWork.GetRepository<MemberShip>()
                .GetAll(x => x.PlanId == planId && x.Status == "Active");
                
            return hasActiveMemberShips.Any();
        }
        #endregion
    }
}
