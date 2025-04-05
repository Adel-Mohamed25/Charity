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
            CreateMap<CreateInKindDonationModel, Domain.Entities.InKindDonation>()
                .ForMember(des => des.IsAllocated, opt => opt.MapFrom(src => false));

            CreateMap<InKindDonationModel, Domain.Entities.InKindDonation>().ReverseMap();

            CreateMap<UpdateInKindDonationModel, Domain.Entities.InKindDonation>().ReverseMap();
        }
    }
}
