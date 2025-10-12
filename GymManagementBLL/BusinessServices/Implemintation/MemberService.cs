using GymManagementBLL.BusinessServices.Interfaces;
using GymManagementBLL.View_Models;
using GymManagementDAL.Repositories.Implemintation;
using GymManagementDAL.Repositories.Interfaces;
using GymManagementDAL.UnitOfWork;
using GymManagmentDAL.Models;
using GymManagmentDAL.Models.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManagementBLL.BusinessServices.Implemintation
{
    internal class MemberService : IMemberService
    {
        private readonly IUnitOfWork _unitOfWork;

        public MemberService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }


        public IEnumerable<MemberViewModel> GetAllMembers()
        {
            var members = _unitOfWork.GetRepository<Member>().GetAll();
            if (members is null || !members.Any()) return [];


            #region Manual First Approach

            //var ListOfMembersViewModels = new List<MemberViewModel>();
            //foreach (var member in members)
            //{
            //    var memberViewModel = new MemberViewModel
            //    {
            //        Id = member.Id,
            //        Name = member.Name,
            //        Email = member.Email,
            //        Phone = member.Phone,
            //        Photo = member.Photo,
            //        Gender = member.Gender.ToString()
            //    };
            //    ListOfMembersViewModels.Add(memberViewModel);
            //}
            //return ListOfMembersViewModels;

            #endregion


            #region Manual Second Approach
            var MembersViewModels = members.Select(m => new MemberViewModel
            {
                Id = m.Id,
                Name = m.Name,
                Email = m.Email,
                Phone = m.Phone,
                Photo = m.Photo,
                Gender = m.Gender.ToString()

            });
            return MembersViewModels;
            #endregion
        }


        public bool CreateMember(CreateMemberViewModel createMember)
        {
            // Email Not Exist Before
            // Phone Not Exist Before
            var EmailExist = _unitOfWork.GetRepository<Member>()
                .GetAll(x => x.Email == createMember.Email).Any();
            var PhoneExist = _unitOfWork.GetRepository<Member>()
                .GetAll(x => x.Phone == createMember.Phone).Any();

            if (EmailExist || PhoneExist) return false;


            // Create MemberViewModel => Member

            var member = new Member
            {
                Name = createMember.Name,
                Email = createMember.Email,
                Phone = createMember.Phone,
                DateOfBirth = createMember.DateOfBirth,
                Gender = createMember.Gender,
                Address =
                {
                    BuildingNumber = createMember.BuildingNumber,
                    City = createMember.City,
                    Street = createMember.Street
                },
                HealthRecord = new HealthRecord
                {
                    Height = createMember.HealthRecord.Height,
                    Weight = createMember.HealthRecord.Weight,
                    BloodType = createMember.HealthRecord.BloodType,
                    Note = createMember.HealthRecord.Note,
                }


            };
            // add to database
            _unitOfWork.GetRepository<Member>().Add(member);
            return _unitOfWork.SaveChanges()>0;


        }

        public MemberViewModel? GetMemberDetails(int memberid)
        {
            var member = _unitOfWork.GetRepository<Member>().GetById(memberid);

            if (member is null) return null;
            var memberViewModel = new MemberViewModel
            {
                Name = member.Name,
                Email = member.Email,
                Phone = member.Phone,
                Photo = member.Photo,
                Gender = member.Gender.ToString(),
                DateOfBirth = member.DateOfBirth.ToShortDateString(),
                Address = $"{member.Address.BuildingNumber}-{member.Address.Street}-{member.Address.City}",

            };

            // active membership
            var membership = _unitOfWork.GetRepository<MemberShip>()
                .GetAll(x => x.MemberId == memberid && x.Status == "Active")
                .FirstOrDefault();

            if (membership is not null)
            {
                memberViewModel.MemberShipStartDate = membership.CreatedAt.ToShortDateString();
                memberViewModel.MemberShipEndDate = membership.EndDate.ToShortDateString();

                var plan = _unitOfWork.GetRepository<Plan>().GetById(membership.PlanId);
                memberViewModel.PlanName = plan?.Name;
            }
            return memberViewModel;
        }

        public HealthRecordView? GetMemberHealthDetails(int memberid)
        {
            var memberHealthRecord = _unitOfWork.GetRepository<HealthRecord>().GetById(memberid);
            if (memberHealthRecord is null) return null;
            return new HealthRecordView
            {
                Weight = memberHealthRecord.Weight,
                Height = memberHealthRecord.Height,
                BloodType = memberHealthRecord.BloodType,
                Note = memberHealthRecord.Note
            };

        }

        public MemberToUpdateViewModel? GetMemberDetailsToUpdate(int memberid)
        {
            var member = _unitOfWork.GetRepository<Member>().GetById(memberid);
            if (member is null) return null;
            return new MemberToUpdateViewModel
            {
                Name = member.Name,
                Photo = member.Photo,
                Email = member.Email,
                Phone = member.Phone,
                BuildingNumber = member.Address.BuildingNumber,
                City = member.Address.City,
                Street = member.Address.Street
            };
        }

        public bool UpdateMember(int memberId, MemberToUpdateViewModel memberToUpdate)
        {
            try
            {
                var EmailExist = _unitOfWork.GetRepository<Member>()
                    .GetAll(x => x.Email == memberToUpdate.Email).Any();
                var PhoneExist = _unitOfWork.GetRepository<Member>()
                    .GetAll(x => x.Phone == memberToUpdate.Phone).Any();
                if (EmailExist || PhoneExist) return false;

                var member = _unitOfWork.GetRepository<Member>().GetById(memberId);
                if (member is null) return false;
                member.Email = memberToUpdate.Email;
                member.Phone = memberToUpdate.Phone;
                member.Address.BuildingNumber = memberToUpdate.BuildingNumber;
                member.Address.City = memberToUpdate.City;
                member.Address.Street = memberToUpdate.Street;
                member.UpdatedAt = DateTime.Now; // the updatedAt was allow null in database , createdAt was auto

                _unitOfWork.GetRepository<Member>().Update(member);
                return _unitOfWork.SaveChanges() > 0;
            }
            catch (Exception)
            {

                return false;
            }


        }
        public bool RemoveMember(int memberId)
        {
            try
            {
                var member = _unitOfWork.GetRepository<Member>().GetById(memberId);
                if (member is null) return false;

                var MemberSessionsId = _unitOfWork.GetRepository<MemberSessions>()
                    .GetAll(x => x.MemberId == memberId)
                    .Select(x => x.SessionId);

                var hasFutureSessions = _unitOfWork.GetRepository<Session>()
                    .GetAll(x => MemberSessionsId.Contains(x.Id) && x.StartDate > DateTime.Now)
                    .Any();

                if (hasFutureSessions) return false;

                _unitOfWork.GetRepository<Member>().Delete(member);
                return _unitOfWork.SaveChanges() > 0;
            }
            catch (Exception)
            {

                return false;
            }

        }
        private bool IsEmailExist(string email)
        {
            return _unitOfWork.GetRepository<Member>().GetAll(x => x.Email == email).Any();
        }
        private bool IsPhoneExist(string phone)
        {
            return _unitOfWork.GetRepository<Member>().GetAll(x => x.Phone == phone).Any();
        } // to use in phone and email validation as short method

    }
}
