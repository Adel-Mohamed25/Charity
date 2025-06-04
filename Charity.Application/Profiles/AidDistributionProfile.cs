using AutoMapper;
using Charity.Domain.Entities;
using Charity.Domain.Enum;
using Charity.Models.AidDistribution;

namespace Charity.Application.Profiles
{
    public class AidDistributionProfile : Profile
    {
        public AidDistributionProfile()
        {
            Map();
        }

        private void Map()
        {
            CreateMap<AidDistribution, CreateAidDistributionModel>();
            CreateMap<CreateAidDistributionModel, AidDistribution>()
                .ForMember(des => des.CreatedDate, opt => opt.MapFrom(src => DateTime.Now))
                .ForMember(des => des.Status, opt => opt.MapFrom(src => AidDistributionStatus.InProgress));

            CreateMap<AidDistribution, UpdateAidDistributionModel>();
            CreateMap<UpdateAidDistributionModel, AidDistribution>()
                .ForMember(des => des.ModifiedDate, opt => opt.MapFrom(src => DateTime.Now));

            CreateMap<AidDistribution, AidDistributionModel>().ReverseMap();
        }
    }
}
