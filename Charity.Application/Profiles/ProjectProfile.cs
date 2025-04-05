using AutoMapper;
using Charity.Domain.Entities;
using Charity.Domain.Enum;
using Charity.Models.Project;

namespace Charity.Application.Profiles
{
    public class ProjectProfile : Profile
    {
        public ProjectProfile()
        {
            Map();
        }

        private void Map()
        {
            CreateMap<CharityProject, CreateProjectModel>();
            CreateMap<CreateProjectModel, CharityProject>()
                .ForMember(des => des.ProjectStatus, opt => opt.MapFrom(src => ProjectStatus.Ongoing))
                .ForMember(des => des.CreatedDate, opt => opt.MapFrom(src => DateTime.Now));

            CreateMap<CharityProject, ProjectModel>().ReverseMap();

            CreateMap<CharityProject, UpdateProjectModel>();
            CreateMap<UpdateProjectModel, CharityProject>()
                .ForMember(des => des.ModifiedDate, opt => opt.MapFrom(src => DateTime.Now));


        }
    }
}
