using AutoMapper;
using GymManagementBLL.BusinessServices.Interfaces;
using GymManagementBLL.View_Models.SessionVM;
using GymManagementDAL.UnitOfWork;
using GymManagmentDAL.Models;
using Microsoft.EntityFrameworkCore.Metadata;
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
        private readonly IMapper _mapper;

        public SessionService(IUnitOfWork uinitOfWork,IMapper mapper)
        {
            _uinitOfWork = uinitOfWork;
            _mapper = mapper;
        }
        public IEnumerable<SessionViewModel> GetAllSessions()
        {
            var sessionRepo= _uinitOfWork.SessionsRepository;
            var sessions = sessionRepo.GetAllWithCategoryAndTrainer();
            if(!sessions.Any()) return [];

            #region Manual Mapping
            //return sessions.Select(s => new SessionViewModel
            //{
            //    ID = s.Id,
            //    CategoryName = s.Category.CategoryName,
            //    Description = s.Description,
            //    TrainerName = s.Trainers.Name,
            //    StartDate = s.StartDate,
            //    EndDate = s.EndDate,
            //    Capacity = s.Capacity,
            //    AvailableSlots = s.Capacity-sessionRepo.GetCountOfBookesSlots(s.Id)
            //}); 
            #endregion

            // AutoMapper

            var MappedSessions = _mapper.Map<IEnumerable<Session>, IEnumerable<SessionViewModel>>(sessions);
            foreach (var session in MappedSessions)
            {
                session.AvailableSlots = session.Capacity - sessionRepo.GetCountOfBookesSlots(session.ID);
            }
            return MappedSessions;
        }

        public SessionViewModel? GetSessionsDetails(int sessionId)
        {
            var sessionRepo = _uinitOfWork.SessionsRepository;
            var sessions= sessionRepo
                .GetByIdWithTrainerAndCategory(sessionId);// use sessionrepo or getrepo.generic
            if (sessions == null) return null;

            #region Manual Mapping
            //return new SessionViewModel
            //{
            //    ID = sessions.Id,
            //    CategoryName = sessions.Category.CategoryName,
            //    Description = sessions.Description,
            //    TrainerName = sessions.Trainers.Name,
            //    StartDate = sessions.StartDate,
            //    EndDate = sessions.EndDate,
            //    Capacity = sessions.Capacity,
            //    AvailableSlots = sessions.Capacity - sessionRepo.GetCountOfBookesSlots(sessions.Id)
            //}; 
            #endregion

            // AutoMapper
            var MappedSession = _mapper.Map<Session,SessionViewModel>(sessions);
            MappedSession.AvailableSlots = sessions.Capacity - sessionRepo.GetCountOfBookesSlots(sessions.Id);
            return MappedSession;
        }
    }
}
