using AutoMapper;
using WSS.API.Domain.Entities;
using WSS.API.Domain.Enums;
using WSS.API.DTOs.Requests;
using WSS.API.DTOs.Responses;

namespace WSS.API.Mapping.Profiles
{
    public class ChangeRequestProfile : Profile
    {
        public ChangeRequestProfile()
        {
            CreateMap<ScheduleChangeRequest, ChangeRequestResponse>();
            CreateMap<CreateChangeRequestRequest, ScheduleChangeRequest>()
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.ScheduleId, opt => opt.Ignore())
                .ForMember(dest => dest.RequestedBy, opt => opt.Ignore())
                .ForMember(dest => dest.RequestedAt, opt => opt.Ignore())
                .ForMember(dest => dest.Status, opt => opt.MapFrom(_ => ChangeRequestStatus.Pending));
        }
    }
}
