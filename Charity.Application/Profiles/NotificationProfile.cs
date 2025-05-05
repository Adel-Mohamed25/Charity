using AutoMapper;
using Charity.Domain.Entities;
using Charity.Models.Notification;

namespace Charity.Application.Profiles
{
    public class NotificationProfile : Profile
    {
        public NotificationProfile()
        {
            Map();
        }

        private void Map()
        {
            CreateMap<Notification, NotificationModel>().ReverseMap();

            CreateMap<Notification, CreateNotificationModel>();
            CreateMap<CreateNotificationModel, Notification>()
                .ForMember(des => des.CreatedDate, opt => opt.MapFrom(src => DateTime.Now))
                .ForMember(des => des.IsRead, opt => opt.MapFrom(src => false));
        }
    }
}
