using AutoMapper;
using GymManagementBLL.BusinessServices.Interfaces;
using GymManagementBLL.View_Models.SessionVM;
using GymManagementDAL.UnitOfWork;
using GymManagementSystemBLL.View_Models.SessionVm;
using GymManagmentDAL.Models;
using Microsoft.EntityFrameworkCore.Metadata;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManagementBLL.BusinessServices.Implemintation
{
    public class SessionService : ISessionService
      
    {
        private readonly IUnitOfWork _uinitOfWork;
        private readonly IMapper _mapper;

        public SessionService(IUnitOfWork uinitOfWork,IMapper mapper)
        {
            _uinitOfWork = uinitOfWork;
            _mapper = mapper;
        }

        public bool CreateSession(CreateSessionViewModel createSessionViewModel)
        {
            try
            {
                if (!IsTrainerExist(createSessionViewModel.TrainerId)) return false;
                if (!IsCategoryExist(createSessionViewModel.CategoryId)) return false;
                if (!IsDateTimeValid(createSessionViewModel.StartDate, createSessionViewModel.EndDate)) return false;
                if (createSessionViewModel.Capacity > 25 || createSessionViewModel.Capacity < 0) return false;

                //var mappedSessionToCreate = _mapper.Map<CreateSessionViewModel, Session>(createSessionViewModel);

                var mappedSessionToCreate = _mapper.Map<Session>(createSessionViewModel);// here he know the source 
                
                _uinitOfWork.GetRepository<Session>().Add(mappedSessionToCreate);
                return _uinitOfWork.SaveChanges() > 0;
            }
            catch (Exception)
            {
                Console.WriteLine("Create Failed");
                return false;
            }
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


        public UpdateSessionViewModel? GetSessionToUpdate(int sessionId)
        {
            var sessionRepo = _uinitOfWork.SessionsRepository;
            var sessionToUpdate = sessionRepo.GetById(sessionId);
            if (!ISessionAvaliableForUpdate(sessionToUpdate!)) return null;
            return _mapper.Map<Session, UpdateSessionViewModel>(sessionToUpdate!);
             
        }

        public bool UpdateSession(int sessionId, UpdateSessionViewModel updateSessionViewModel)
        {
            try
            {
                var sessionRepo = _uinitOfWork.SessionsRepository;
                var sessionToUpdate = sessionRepo.GetById(sessionId);

                if (!ISessionAvaliableForUpdate(sessionToUpdate!)) return false;
                if (!IsTrainerExist(updateSessionViewModel.TrainerId)) return false;
                if (!IsDateTimeValid(updateSessionViewModel.StartDate, updateSessionViewModel.EndDate)) return false;

                var MappedSessionToUpdate = _mapper.Map(updateSessionViewModel,sessionToUpdate); // recomended way to map
                sessionRepo.Update(MappedSessionToUpdate!);
                MappedSessionToUpdate!.UpdatedAt = DateTime.Now;
                return _uinitOfWork.SaveChanges() > 0;
            }
            catch (Exception)
            {
                Console.WriteLine("Failed To Update");

                return false;
            }

        }
        public bool DeleteSession(int sessionId)
        {
            var sessionRepo = _uinitOfWork.SessionsRepository;
            var sessionToDelete = sessionRepo.GetById(sessionId);
            if(!ISessionAvaliableForDelete(sessionToDelete!))return false;
            sessionRepo.Delete(sessionToDelete!);
            return _uinitOfWork.SaveChanges() > 0;
        }

        #region HelperMethod
        private bool  IsTrainerExist(int trainerId)
        {
            return _uinitOfWork.GetRepository<Trainer>().GetById(trainerId) is not null;
        } 
        private bool  IsCategoryExist(int categoryId)
        {
            return _uinitOfWork.GetRepository<Category>().GetById(categoryId) is not null;
        } 

        private bool IsDateTimeValid(DateTime start, DateTime end)
        {
            return end > start;
        }

        private bool ISessionAvaliableForUpdate(Session session)
        {
            if(session ==null) return false;
            if(session.StartDate <= DateTime.Now) return false;
            if(session.EndDate<=DateTime.Now) return false;
            var HasActiveBooking = _uinitOfWork.SessionsRepository
                .GetCountOfBookesSlots(session.Id) > 0;
            if (HasActiveBooking) return false; 
            return true;
        }
        private bool ISessionAvaliableForDelete(Session session)
        {
            if(session ==null) return false;
            if(session.EndDate<=DateTime.Now && session.EndDate>DateTime.Now) return false;
            var HasActiveBooking = _uinitOfWork.SessionsRepository
                .GetCountOfBookesSlots(session.Id) > 0;
            if (HasActiveBooking) return false; 
            return true;
        }


        #endregion
    }
}
