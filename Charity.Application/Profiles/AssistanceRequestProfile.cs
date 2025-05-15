using AutoMapper;
using Charity.Domain.Entities;
using Charity.Domain.Enum;
using Charity.Models.AssistanceRequest;

namespace Charity.Application.Profiles
{
    public class AssistanceRequestProfile : Profile
    {
        public AssistanceRequestProfile()
        {
            Map();
        }

        private void Map()
        {
            CreateMap<AssistanceRequest, CreateAssistanceRequestModel>();
            CreateMap<CreateAssistanceRequestModel, AssistanceRequest>()
                .ForMember(des => des.RequestStatus, opt => opt.MapFrom(src => RequestStatus.Pending));

            CreateMap<AssistanceRequest, UpdateAssistanceRequestModel>();
            CreateMap<UpdateAssistanceRequestModel, AssistanceRequest>()
                .ForMember(des => des.ModifiedDate, opt => opt.MapFrom(src => DateTime.Now));

            CreateMap<AssistanceRequest, AssistanceRequestModel>().ReverseMap();
        }
    }
}
