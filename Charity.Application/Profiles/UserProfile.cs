using AutoMapper;
using Charity.Domain.Entities.IdentityEntities;
using Charity.Models.User;

namespace Charity.Application.Mapper
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            Map();
        }

        private void Map()
        {
            CreateMap<CharityUser, CreateUserModel>();
            CreateMap<CreateUserModel, CharityUser>()
                .ForMember(des => des.CreatedDate, opt => opt.MapFrom(src => DateTime.Now));

            CreateMap<UserModel, CharityUser>();
            CreateMap<CharityUser, UserModel>()
                .ForMember(dest => dest.Age, opt => opt.MapFrom(src =>
                    src.DateOfBirth.HasValue ? (int)((DateTime.UtcNow - src.DateOfBirth.Value).TotalDays / 365.25) : 0));

            CreateMap<CharityUser, UpdateUserModel>();
            CreateMap<UpdateUserModel, CharityUser>()
                .ForMember(des => des.ModifiedDate, opt => opt.MapFrom(src => DateTime.Now))
                .ForMember(des => des.UserName, opt => opt.MapFrom(src => src.Email));

        }
    }
}
