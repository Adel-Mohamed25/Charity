using AutoMapper;
using Charity.Domain.Entities;
using Charity.Models.InKindDonation;

namespace Charity.Application.Profiles
{
    public class InKindDonationProfile : Profile
    {
        public InKindDonationProfile()
        {
            Map();
        }

        private void Map()
        {
            CreateMap<InKindDonation, CreateInKindDonationModel>();
            CreateMap<CreateInKindDonationModel, InKindDonation>()
                .ForMember(des => des.CreatedDate, opt => opt.MapFrom(src => DateTime.Now))
                .ForMember(des => des.IsAllocated, opt => opt.MapFrom(src => false));

            CreateMap<InKindDonation, UpdateInKindDonationModel>();
            CreateMap<UpdateInKindDonationModel, InKindDonation>()
                .ForMember(des => des.ModifiedDate, opt => opt.MapFrom(src => DateTime.Now));

            CreateMap<InKindDonationModel, InKindDonation>().ReverseMap();

        }
    }
}
