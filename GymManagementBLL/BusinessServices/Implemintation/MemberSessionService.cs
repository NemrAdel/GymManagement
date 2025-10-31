using AutoMapper;
using GymManagementBLL.BusinessServices.Interfaces;
using GymManagementBLL.View_Models.CategoryVM.MemberSessionviewModel;
using GymManagementBLL.View_Models.MemberSessionviewModel;
using GymManagementDAL.Repositories.Interfaces;
using GymManagementDAL.UnitOfWork;
using GymManagmentDAL.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManagementBLL.BusinessServices.Implemintation
{
    public class MemberSessionService : IMemberSessionService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public MemberSessionService(IUnitOfWork unitOfWork , IMapper mapper )
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public IEnumerable<MemberSessionViewModel> GetAllMemberSession()
        {
            var MemberSessions = _unitOfWork.MemberSessionRepository.GetAllMemberSessionsWithMemberAndSessionAndCategoryAndTrainer();
            if (MemberSessions is null || !MemberSessions.Any())
                return [];
            var mappedMemberSessions = _mapper.Map<IEnumerable<MemberSessionViewModel>>(MemberSessions);
            foreach(var session in mappedMemberSessions)
            {
                session.AvalibaleSlots= session.Capacity - _unitOfWork.MemberSessionRepository.GetCountOfBookesSlots(session.SessionId);
            }
            return mappedMemberSessions;

        }
        public IEnumerable<OnGoingViewModel> GetOnGoingSessionsBySessionId(int id)
        {
            var onGoingSessions = _unitOfWork.MemberSessionRepository.GetMemberSessionswithMembers();
            onGoingSessions = onGoingSessions.Where(ms => ms.SessionId == id);
            if (onGoingSessions is null)
            {
                return [];
            }
            var mappedOnGoingSessions = _mapper.Map<IEnumerable<OnGoingViewModel>>(onGoingSessions);
            return mappedOnGoingSessions;
        }

        public MemberSessions Attendance(int id)
        {
            var memberSession = _unitOfWork.MemberSessionRepository.getMemberSessionById(id);
            if (memberSession == null)
            {
                Console.WriteLine($"MemberSession not found. {id}");
                return null;
            }
            memberSession.IsIttended = !memberSession.IsIttended;
            _unitOfWork.GetRepository<MemberSessions>().Update(memberSession);
            _unitOfWork.SaveChanges();
            return memberSession;
        }

        public IEnumerable<UpComingViewModel> GetMemberSessionsWithMembersAndSessions()
        {
            var memberSessions = _unitOfWork.MemberSessionRepository.GetMemberSessionWithMemberAndSession();
            if (memberSessions is null)
                return [];
            var mappedMemberSessions = _mapper.Map<IEnumerable<UpComingViewModel>>(memberSessions);
            return mappedMemberSessions;
        }
    }
}
