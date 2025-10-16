using AutoMapper;
using GymManagementBLL.View_Models.SessionVM;
using GymManagementSystemBLL.View_Models.SessionVm;
using GymManagmentDAL.Models;

namespace GymManagementPL.Mapping
{
    public class MappingProfile:Profile
    {
        public MappingProfile()
        {
            CreateMap<Session, SessionViewModel>()
                .ForMember(dest => dest.TrainerName, options =>
                    options.MapFrom(src => src.Trainers.Name))
                //مش شرط احط {} لو مفيش غير سطر واحد
                .ForMember(dest => dest.CategoryName, options =>
                    options.MapFrom(src => src.Category.CategoryName))
                .ForMember(dest => dest.AvailableSlots, options =>
                    options.Ignore()); // AvailableSlots will be set manually in the service


            CreateMap<Session, CreateSessionViewModel>();
            CreateMap<Session, UpdateSessionViewModel>();
        }
    }
}
