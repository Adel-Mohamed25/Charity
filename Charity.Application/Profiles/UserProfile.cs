using AutoMapper;
using Charity.Domain.Entities.IdentityEntities;
using Charity.Models.User;

namespace Charity.Application.Mapper
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            Mapp();
        }

        private void Mapp()
        {
            CreateMap<CreateUserModel, User>();
            CreateMap<User, CreateUserModel>();

            CreateMap<UserModel, User>();
            CreateMap<User, UserModel>();
        }
    }
}
