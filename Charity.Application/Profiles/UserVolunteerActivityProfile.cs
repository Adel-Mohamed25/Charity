using AutoMapper;
using Charity.Domain.Entities;
using Charity.Domain.Entities.IdentityEntities;
using Charity.Models.User;
using Charity.Models.UserVolunteerActivity;

namespace Charity.Application.Profiles
{
    public class UserVolunteerActivityProfile : Profile
    {
        public UserVolunteerActivityProfile()
        {
            Map();
        }

        private void Map()
        {
            CreateMap<UserVolunteerActivity, UserVolunteerActivityModel>();
            CreateMap<UserVolunteerActivityModel, UserVolunteerActivity>()
                .ForMember(des => des.JoinDate, opt => opt.MapFrom(src => DateTime.Now));


            CreateMap<CharityUser, VolunteerModel>()
                .ForMember(dest => dest.Age, opt => opt.MapFrom(src =>
                    src.DateOfBirth.HasValue ? (int)((DateTime.UtcNow - src.DateOfBirth.Value).TotalDays / 365.25) : 0));


            CreateMap<UserVolunteerActivity, VolunteerModel>()
                .IncludeMembers(src => src.User)
                .ForMember(dest => dest.JoinDate, opt => opt.MapFrom(src => src.JoinDate));

        }
    }
}
