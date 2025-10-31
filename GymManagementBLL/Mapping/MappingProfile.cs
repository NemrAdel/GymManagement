using AutoMapper;
using GymManagementBLL.View_Models;
using GymManagementBLL.View_Models.CategoryVM.MemberSessionviewModel;
using GymManagementBLL.View_Models.MemberSessionviewModel;
using GymManagementBLL.View_Models.MemberShipVM;
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
            MapMemberShip();
            MapMemberSession();


        }

        private void MapSession()
        {
            CreateMap<Session, SessionViewModel>()
            .ForMember(dest => dest.TrainerName, options => options.MapFrom(src => src.Trainers.Name))
            .ForMember(dest => dest.CategoryName, options => options.MapFrom(src => src.Category.CategoryName))
            .ForMember(dest => dest.AvailableSlots, options => options.Ignore());


            CreateMap<CreateSessionViewModel, Session>();

            CreateMap<Session, UpdateSessionViewModel>().ReverseMap();

            CreateMap<Category, CategorySelectViewModel>();

            CreateMap<Trainer, TrainerSelectViewModel>();
        }

        private void MapMember()
        {
            CreateMap<CreateMemberViewModel, Member>()
                .ForMember(dest => dest.Address, opt => 
                opt.MapFrom(src => new Address
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
                .ForMember(dest => dest.Name, opt 
                => opt.Ignore())
                .ForMember(dest => dest.Photo, opt
                => opt.Ignore())
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

        private void MapMemberShip()
        {
            CreateMap<MemberShip,MemberShipViewModel>()
                .ForMember(dest => dest.MName, opt => opt.MapFrom(src => src.Member.Name))
                .ForMember(dest => dest.PName, opt => opt.MapFrom(src => src.Plan.Name))
                .ForMember(dest => dest.StartDate, opt => opt.MapFrom(src => src.CreatedAt.ToShortDateString()))
                .ForMember(dest => dest.EndDate, opt => opt.MapFrom(src => src.EndDate.ToShortDateString()));

            CreateMap<CreateMemberShipViewModel, MemberShip>();
                //.ForMember(dest => dest.MemberId, opt =>
                //opt.MapFrom(src => src.MemberId))
                //.ForMember(dest => dest.PlanId, opt =>
                //opt.MapFrom(src => src.PlanId));
        }

        private void MapMemberSession()
        {
            CreateMap<MemberSessions, MemberSessionViewModel>()
                .ForMember(dest => dest.TrainerName, opt =>
                opt.MapFrom(src => src.Sessions.Trainers.Name))
                .ForMember(dest => dest.CategoryName, opt =>
                opt.MapFrom(src => src.Sessions.Category.CategoryName))
                .ForMember(dest => dest.Capacity, opt =>
                opt.MapFrom(src => src.Sessions.Capacity))
                .ForMember(dest => dest.StartDate, opt =>
                opt.MapFrom(src => src.Sessions.StartDate))
                .ForMember(dest => dest.EndDate, opt =>
                opt.MapFrom(src => src.Sessions.EndDate));

            CreateMap<MemberSessions, OnGoingViewModel>()
                .ForMember(dest => dest.Name, opt =>
                opt.MapFrom(src => src.Members.Name))
                .ForMember(dest => dest.Attendance, opt =>
                opt.MapFrom(src => src.IsIttended.ToString()))
                .ForMember(dest => dest.Id, opt =>
                opt.MapFrom(src => src.MemberId));

            CreateMap<MemberSessions, UpComingViewModel>()
                .ForMember(dest => dest.Name, opt =>
                opt.MapFrom(src => src.Members.Name))
                .ForMember(dest => dest.BookingDate, opt =>
                opt.MapFrom(src => src.Sessions.StartDate.ToString()));


            CreateMap<CreateMemberSessionViewModel, MemberSessions>()
                .ForMember(dest => dest.MemberId, opt =>
                opt.MapFrom(src => src.MemberId))
                .ForMember(dest => dest.SessionId, opt =>
                opt.MapFrom(src => src.SessionId));



        }


    }
}