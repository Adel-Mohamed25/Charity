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
            CreateMap<CharityRole, CreateRoleModel>().ReverseMap();

            CreateMap<CharityRole, RoleModel>().ReverseMap();
        }
    }
}
