using AutoMapper;
using GymManagementBLL.View_Models;
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


            CreateMap<CreateSessionViewModel, Session>();
            CreateMap<Session, UpdateSessionViewModel>().ReverseMap();
            //CreateMap<UpdateSessionViewModel, Session>();


            CreateMap<Member, MemberViewModel>()
                .ForMember(dest=>dest.DateOfBirth,option=>
                option.MapFrom(src=>src.DateOfBirth.ToShortDateString()))

                .ForMember(dest => dest.Address, options =>
                    options.MapFrom(src =>src.Address !=null? $"{src.Address.BuildingNumber}-{src.Address.Street}-{src.Address.City}":"Address not found"));

            CreateMap<CreateMemberViewModel, HealthRecord>();

            CreateMap<CreateMemberViewModel, Member>()
                //.ForPath(dest=>dest.HealthRecord.BloodType,options=>
                //options.MapFrom(src=>src.HealthRecord))
                //.ForMember(dest=>dest.HealthRecord.Height,options=>
                //options.MapFrom(src=>src.HealthRecord.Height))
                //.ForMember(dest=>dest.HealthRecord.Weight,options=>
                //options.MapFrom(src=>src.HealthRecord.Weight))
                //.ForMember(dest=>dest.HealthRecord.Note,options=>
                //options.MapFrom(src=>src.HealthRecord.Note));
                .ForMember(dest => dest.HealthRecord, options =>
                options.MapFrom(src => src));


            CreateMap<Member, MemberViewModel>().ReverseMap();

            CreateMap<HealthRecord, HealthRecordView>();



            CreateMap<Member, MemberToUpdateViewModel>()
                .ForMember(dest => dest.BuildingNumber, options =>
                    options.MapFrom(src => src.Address.BuildingNumber))
                .ForMember(dest => dest.City, options =>
                    options.MapFrom(src => src.Address.City))
                .ForMember(dest => dest.Street, options =>
                    options.MapFrom(src => src.Address.Street));


            CreateMap<IEnumerable<PlanViewModel>, IEnumerable<Plan>>();
            CreateMap<Plan, PlanToUpdateViewModel>();

            CreateMap<Trainer, TrainerViewModel>();


            CreateMap<IEnumerable<Trainer>, IEnumerable<TrainerViewModel>>();

            CreateMap<CreateTrainerViewModel, Address>();
            CreateMap<CreateTrainerViewModel, Trainer>()
                //.ForMember(dest => dest.Address.BuildingNumber, options =>
                //options.MapFrom(src => src.BuildingNumber))
                //.ForMember(dest => dest.Address.Street, options =>
                //options.MapFrom(src => src.Street))
                //.ForMember(dest => dest.Address.City, options =>
                //options.MapFrom(src => src.City));
                .ForMember(dest => dest.Address, options =>
                options.MapFrom(src => src));


            CreateMap<TrainerToUpdateViewModel, Address>();
            CreateMap<TrainerToUpdateViewModel, Trainer>()
                //.ForMember(dest => dest.Address.BuildingNumber, options =>
                //options.MapFrom(src => src.BuildingNumber))
                //.ForMember(dest => dest.Address.Street, options =>
                //options.MapFrom(src => src.Street))
                //.ForMember(dest => dest.Address.City, options =>
                //options.MapFrom(src => src.City)).ReverseMap();
                .ForMember(dest => dest.Address, options =>
                options.MapFrom(src => src));
        }
    }
}
