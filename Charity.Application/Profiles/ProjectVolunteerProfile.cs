using AutoMapper;
using Charity.Domain.Entities;
using Charity.Domain.Entities.IdentityEntities;
using Charity.Models.ProjectVolunteer;
using Charity.Models.User;

namespace Charity.Application.Profiles
{
    public class ProjectVolunteerProfile : Profile
    {
        public ProjectVolunteerProfile()
        {
            Map();
        }

        private void Map()
        {
            CreateMap<ProjectVolunteer, ProjectVolunteerModel>();
            CreateMap<ProjectVolunteerModel, ProjectVolunteer>()
                .ForMember(des => des.JoinDate, opt => opt.MapFrom(src => DateTime.Now));


            CreateMap<CharityUser, VolunteerModel>()
                .ForMember(dest => dest.Age, opt => opt.MapFrom(src =>
                    src.DateOfBirth.HasValue ? (int)((DateTime.UtcNow - src.DateOfBirth.Value).TotalDays / 365.25) : 0));


            CreateMap<ProjectVolunteer, VolunteerModel>()
                .IncludeMembers(src => src.Volunteer)
                .ForMember(dest => dest.JoinDate, opt => opt.MapFrom(src => src.JoinDate));

        }
    }
}
