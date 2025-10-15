using GymManagementBLL.BusinessServices.Interfaces;
using GymManagementBLL.View_Models.SessionVM;
using GymManagementDAL.UnitOfWork;
using GymManagmentDAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManagementBLL.BusinessServices.Implemintation
{
    internal class SessionService : ISessionService
      
    {
        private readonly IUnitOfWork _uinitOfWork;

        public SessionService(IUnitOfWork uinitOfWork)
        {
            _uinitOfWork = uinitOfWork;
        }
        public IEnumerable<SessionViewModel> GetAllSessions()
        {
            var sessionRepo= _uinitOfWork.SessionsRepository;
            var sessions = sessionRepo.GetAllWithCategoryAndTrainer();
            if(!sessions.Any()) return [];
            return sessions.Select(s => new SessionViewModel
            {
                ID = s.Id,
                CategoryName = s.Category.CategoryName,
                Description = s.Description,
                TrainerName = s.Trainers.Name,
                StartDate = s.StartDate,
                EndDate = s.EndDate,
                Capacity = s.Capacity,
                AvailableSlots = s.Capacity-sessionRepo.GetCountOfBookesSlots(s.Id)
            });
        }
    }
}
