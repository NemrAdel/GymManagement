using AutoMapper;
using GymManagementBLL.View_Models;
using GymManagementBLL.View_Models.SessionVM;
using GymManagementSystemBLL.View_Models.SessionVm;
using GymManagmentDAL.Models;

namespace GymManagementPL.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {

            MapSession();
            MapMember();
            MapPlan();
            MapTrainer();


        }

        private void MapSession()
        {
            CreateMap<Session, SessionViewModel>()
    .ForMember(dest => dest.TrainerName, options => options.MapFrom(src => src.Trainers.Name))
    .ForMember(dest => dest.CategoryName, options => options.MapFrom(src => src.Category.CategoryName))
    .ForMember(dest => dest.AvailableSlots, options => options.Ignore());


            CreateMap<CreateSessionViewModel, Session>();

            CreateMap<Session, UpdateSessionViewModel>().ReverseMap();
        }

        private void MapMember()
        {
            CreateMap<CreateMemberViewModel, Member>()
                .ForMember(dest => dest.Address, opt => opt.MapFrom(src => new Address
                {
                    BuildingNumber = src.BuildingNumber,
                    Street = src.Street,
                    City = src.City,
                }));

            CreateMap<HealthRecordView, HealthRecord>()
                .ReverseMap();

            CreateMap<Member, MemberViewModel>()
                .ForMember(dest => dest.DateOfBirth, opt => opt.MapFrom(src => src.DateOfBirth.ToShortDateString()))
                .ForMember(dest => dest.Address, opt => opt.MapFrom(src => $"{src.Address.BuildingNumber}-{src.Address.Street}-{src.Address.City}"));

            #region Second Way
            //CreateMap<CreateMemberViewModel, Member>()
            //    .ForMember(dest => dest.Address, opt => opt.MapFrom(src => src));

            //CreateMap<CreateMemberViewModel, Address>()
            //    .ForMember(dest => dest.BuildingNumber, opt => opt.MapFrom(src => src.BuildingNumber))
            //    .ForMember(dest => dest.Street, opt => opt.MapFrom(src => src.Street))
            //    .ForMember(dest => dest.City, opt => opt.MapFrom(src => src.City)); 
            #endregion

            CreateMap<Member, MemberToUpdateViewModel>()
                .ForMember(dest => dest.BuildingNumber, opt => opt.MapFrom(src => src.Address.BuildingNumber))
                .ForMember(dest => dest.Street, opt => opt.MapFrom(src => src.Address.Street))
                .ForMember(dest => dest.City, opt => opt.MapFrom(src => src.Address.City));



            CreateMap<MemberToUpdateViewModel, Member>()
                .ForMember(dest => dest.Name, opt => opt.Ignore())
                .ForMember(dest => dest.Photo, opt => opt.Ignore())
                .AfterMap((src, dest) =>
                {
                    dest.Address.BuildingNumber = src.BuildingNumber;
                    dest.Address.Street = src.Street;
                    dest.Address.City = src.City;
                    dest.UpdatedAt = DateTime.Now;
                });
        }

        private void MapPlan()
        {
            CreateMap<Plan, PlanViewModel>();

            CreateMap<Plan, PlanToUpdateViewModel>();

            CreateMap<PlanToUpdateViewModel, Plan>()
                .ForMember(dest => dest.Name, opt => opt.Ignore())
                .ForMember(dest => dest.UpdatedAt, opt => opt.MapFrom(src => DateTime.Now));
        }
        private void MapTrainer()
        {
            CreateMap<CreateTrainerViewModel, Trainer>()
                .ForMember(dest => dest.Address, opt => opt.MapFrom(src => new Address
                {
                    BuildingNumber = src.BuildingNumber,
                    Street = src.Street,
                    City = src.City
                }));

            CreateMap<Trainer, TrainerViewModel>()
                  .ForMember(dest => dest.DateOfBirth, opt => opt.MapFrom(src => src.DateOfBirth.ToShortDateString()))
                 .ForMember(dest => dest.Address, opt => opt.MapFrom(src => $"{src.Address.BuildingNumber}-{src.Address.Street}-{src.Address.City}")); ;

            CreateMap<Trainer, TrainerToUpdateViewModel>()
                .ForMember(dest => dest.Street, opt => opt.MapFrom(src => src.Address.Street))
                .ForMember(dest => dest.City, opt => opt.MapFrom(src => src.Address.City))
                .ForMember(dest => dest.BuildingNumber, opt => opt.MapFrom(src => src.Address.BuildingNumber));

            CreateMap<TrainerToUpdateViewModel, Trainer>()
                .ForMember(dest => dest.Name, opt => opt.Ignore())
                .AfterMap((src, dest) =>
                {
                    dest.Address.BuildingNumber = src.BuildingNumber;
                    dest.Address.City = src.City;
                    dest.Address.Street = src.Street;
                    dest.UpdatedAt = DateTime.Now;
                });

        }


    }
}