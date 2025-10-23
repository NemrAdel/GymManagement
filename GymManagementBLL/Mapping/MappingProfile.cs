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

            
            

            CreateMap<CreateMemberViewModel, Member>()
                .ForMember(dest => dest.Address, options =>
                options.MapFrom(src => src));

            CreateMap<CreateMemberViewModel, Address>()
                .ForMember(dest => dest.BuildingNumber, option =>
                option.MapFrom(src => src.BuildingNumber))
                .ForMember(dest => dest.Street, option =>
                option.MapFrom(src => src.Street))
                .ForMember(dest => dest.City, option =>
                option.MapFrom(src => src.City));

            CreateMap<HealthRecordView, HealthRecord>().ReverseMap();




            CreateMap<Member, MemberToUpdateViewModel>()
                .ForMember(dest => dest.BuildingNumber, options =>
                    options.MapFrom(src => src.Address.BuildingNumber))
                .ForMember(dest => dest.City, options =>
                    options.MapFrom(src => src.Address.City))
                .ForMember(dest => dest.Street, options =>
                    options.MapFrom(src => src.Address.Street));

            CreateMap<MemberToUpdateViewModel, Member>()
                .ForMember(dest => dest.Name, opt =>
                opt.Ignore())
                .ForMember(dest => dest.Photo, opt =>
                opt.Ignore())
                .AfterMap((src, dest) => {
                    dest.Address.Street = src.Street;
                    dest.Address.BuildingNumber = src.BuildingNumber;
                    dest.Address.City = src.City;
                    dest.UpdatedAt = DateTime.Now;
                });
                 


            CreateMap<IEnumerable<PlanViewModel>, IEnumerable<Plan>>();
            CreateMap<Plan, PlanToUpdateViewModel>();



            CreateMap<Trainer, TrainerViewModel>()
                .ForMember(dest=>dest.Address,opt=>
                opt.MapFrom(src=>$"{src.Address.BuildingNumber}-{src.Address.Street}-{src.Address.City}"))
                .ForMember(dest=>dest.DateOfBirth,opt=>
                opt.MapFrom(src=>src.DateOfBirth.ToShortDateString()));


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
