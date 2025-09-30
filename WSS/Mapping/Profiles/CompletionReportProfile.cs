using AutoMapper;
using WSS.API.Domain.Entities;
using WSS.API.DTOs.Requests;

namespace WSS.API.Mapping.Profiles
{
    public class CompletionReportProfile : Profile
    {
        public CompletionReportProfile()
        {
            CreateMap<SubmitCompletionReportRequest, WorkCompletionReport>()
                .ForMember(dest => dest.WorkItemId, opt => opt.Ignore())
                .ForMember(dest => dest.ReportedBy, opt => opt.Ignore())
                .ForMember(dest => dest.ReportedAt, opt => opt.MapFrom(_ => DateTime.UtcNow))
                .ForMember(dest => dest.IsVerified, opt => opt.MapFrom(_ => false));
        }
    }
}
