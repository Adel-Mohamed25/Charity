using AutoMapper;
using Charity.Domain.Entities.IdentityEntities;
using Charity.Models.Role;

namespace Charity.Application.Profiles
{
    public class RoleProfile : Profile
    {
        public RoleProfile()
        {
            Map();
        }

        private void Map()
        {
            CreateMap<CharityRole, CreateRoleModel>();
            CreateMap<CreateRoleModel, CharityRole>()
                .ForMember(des => des.CreatedDate, opt => opt.MapFrom(src => DateTime.Now));

            CreateMap<CharityRole, UpdateRoleModel>();
            CreateMap<UpdateRoleModel, CharityRole>()
                .ForMember(des => des.ModifiedDate, opt => opt.MapFrom(src => DateTime.Now));

            CreateMap<CharityRole, RoleModel>().ReverseMap();
        }
    }
}
