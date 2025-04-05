using AutoMapper;
using Charity.Domain.Entities;
using Charity.Domain.Enum;
using Charity.Models.VolunteerApplication;

namespace Charity.Application.Profiles
{
    public class VolunteerApplicationProfile : Profile
    {
        public VolunteerApplicationProfile()
        {
            Map();
        }

        private void Map()
        {
            CreateMap<VolunteerApplication, CreateVolunteerApplicationModel>();
            CreateMap<CreateVolunteerApplicationModel, VolunteerApplication>()
                .ForMember(des => des.CreatedDate, opt => opt.MapFrom(src => DateTime.Now))
                .ForMember(des => des.RequestStatus, opt => opt.MapFrom(src => RequestStatus.Pending));

            CreateMap<VolunteerApplication, UpdateVolunteerApplicationModel>();
            CreateMap<UpdateVolunteerApplicationModel, VolunteerApplication>()
                .ForMember(des => des.ModifiedDate, opt => opt.MapFrom(src => DateTime.Now));

            CreateMap<VolunteerApplication, VolunteerApplicationModel>().ReverseMap();
        }
    }
}
