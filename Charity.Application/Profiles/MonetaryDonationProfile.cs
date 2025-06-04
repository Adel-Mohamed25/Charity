using AutoMapper;
using Charity.Domain.Entities;
using Charity.Models.MonetaryDonation;

namespace Charity.Application.Profiles
{
    public class MonetaryDonationProfile : Profile
    {
        public MonetaryDonationProfile()
        {
            Map();
        }

        private void Map()
        {
            CreateMap<MonetaryDonation, MonetaryDonationModel>().ReverseMap();
        }
    }
}
