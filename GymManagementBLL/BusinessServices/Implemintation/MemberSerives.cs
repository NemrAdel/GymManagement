using GymManagementBLL.BusinessServices.Interfaces;
using GymManagementBLL.View_Models;
using GymManagementDAL.Repositories.Implemintation;
using GymManagementDAL.Repositories.Interfaces;
using GymManagmentDAL.Models;
using GymManagmentDAL.Models.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManagementBLL.BusinessServices.Implemintation
{
    internal class MemberSerives : IMemberService
    {
        private readonly IGenericRepository<Member> _memebrRepository;

        public MemberSerives(IGenericRepository<Member> memebrRepository)
        {
            _memebrRepository = memebrRepository;
        }
        public IEnumerable<MemberViewModel> GetAllMembers()
        {
            var members = _memebrRepository.GetAll();
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
                Id=m.Id,
                Name=m.Name,
                Email=m.Email,
                Phone = m.Phone,
                Photo = m.Photo,
                Gender = m.Gender.ToString()

            });
            return MembersViewModels;
            #endregion
        }
    }
}
