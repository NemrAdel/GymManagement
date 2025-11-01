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

        public IEnumerable<UpComingViewModel> GetMembersWithSessionId(int id)
        {
            var memberSessions = _unitOfWork.MemberSessionRepository.GetMemberSessionWithMemberAndSession();
            if (memberSessions is null)
                return [];
            var memberBySessionId = memberSessions.Where(ms => ms.SessionId == id); 
            var mappedMemberSessions = _mapper.Map<IEnumerable<UpComingViewModel>>(memberBySessionId);
            return mappedMemberSessions;
        }

        public IEnumerable<MemberSessions> GetAllMemberSessionWithMembers()
        {
            var memberSessions = _unitOfWork.MemberSessionRepository.GetMemberSessionWithMemberAndSession();
            return memberSessions;
        }

        public bool Create( CreateMemberSessionViewModel createMember)
        {
            if (createMember == null)
            {
                return false;
            }
            var mappedMemberSession = new MemberSessions
            {
                IsIttended = false,
                MemberId = createMember.MemberId,
                CreatedAt = DateTime.Now,
                SessionId = createMember.SessionId,

            };
            //mappedMemberSession.IsIttended = false;
            //mappedMemberSession.CreatedAt= DateTime.Now;
            //mappedMemberSession.SessionId = sessionid;
            _unitOfWork.GetRepository<MemberSessions>().Add(mappedMemberSession);
            return _unitOfWork.SaveChanges()>0;
        }

        public IEnumerable<MemberSessions> GetMemberSessionWithMemberAndSession()
        {
            var memberSessions = _unitOfWork.MemberSessionRepository.GetMemberSessionWithMemberAndSession();
            return memberSessions;
        }

        public bool Cancel(int id)
        {
            var memberSession = _unitOfWork.GetRepository<MemberSessions>().GetById(id);
            if (memberSession == null)
            {
                return false;
            }
            _unitOfWork.GetRepository<MemberSessions>().Delete(memberSession);
            return _unitOfWork.SaveChanges() > 0;
        }
    }
}
