using AutoMapper;
using WSS.API.Domain.Entities;
using WSS.API.DTOs.Requests;
using WSS.API.DTOs.Responses;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace WSS.API.Mapping.Profiles
{
    /// <summary>
    /// Профиль маппинга для WorkSchedule ↔ DTO.
    /// </summary>
    public class WorkScheduleProfile : Profile
    {
        public WorkScheduleProfile()
        {
            // Entity → Response
            CreateMap<WorkSchedule, WorkScheduleResponse>();
            CreateMap<WorkItem, WorkItemResponse>();

            // Request → Entity
            CreateMap<CreateWorkItemRequest, WorkItem>()
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.ScheduleId, opt => opt.Ignore())
                .ForMember(dest => dest.Status, opt => opt.MapFrom(_ => WorkItemStatus.Planned));
        }
    }
}
