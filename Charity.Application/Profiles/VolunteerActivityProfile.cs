using AutoMapper;
using Charity.Domain.Entities;
using Charity.Models.VolunteerActivity;

namespace Charity.Application.Profiles
{
    public class VolunteerActivityProfile : Profile
    {
        public VolunteerActivityProfile()
        {
            Map();
        }

        private void Map()
        {
            CreateMap<VolunteerActivity, CreateVolunteerActivityModel>();
            CreateMap<CreateVolunteerActivityModel, VolunteerActivity>()
                .ForMember(des => des.CreatedDate, opt => opt.MapFrom(src => DateTime.Now));

            CreateMap<VolunteerActivity, UpdateVolunteerActivityModel>();
            CreateMap<UpdateVolunteerActivityModel, VolunteerActivity>()
                .ForMember(des => des.ModifiedDate, opt => opt.MapFrom(src => DateTime.Now));

            CreateMap<VolunteerActivity, VolunteerActivityModel>().ReverseMap();
        }
    }
}
