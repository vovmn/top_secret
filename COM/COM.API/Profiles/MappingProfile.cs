using AutoMapper;
using COM.API.Domain.Entities;
using COM.API.Domain.Enums;
using COM.API.Domain.ValueObjects;
using COM.API.DTOs.Requests;
using COM.API.DTOs.Responses;

namespace COM.API.Profiles
{
    /// <summary>
    /// Профиль маппинга AutoMapper для автоматического преобразования между доменными сущностями и DTO.
    /// Упрощает код контроллеров и сервисов, избавляя от ручного копирования свойств.
    /// </summary>
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            // === Domain → Response DTOs ===

            CreateMap<ConstructionObject, ObjectResponse>()
                .ForMember(dest => dest.Polygon, opt => opt.MapFrom(src => src.Polygon.Coordinates.ToList()))
                .ForMember(dest => dest.Responsibles, opt => opt.MapFrom(src => src.Responsibles))
                .ForMember(dest => dest.Checklists, opt => opt.MapFrom(src => src.Checklists));

            CreateMap<ObjectResponsible, ResponsibleDto>();

            CreateMap<Checklist, ChecklistDto>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id.ToString()));

            // === Request DTOs → Domain Entities ===

            // CreateObjectRequest → ConstructionObject
            CreateMap<CreateObjectRequest, ConstructionObject>()
                .ConstructUsing(src => new ConstructionObject(
                    Guid.NewGuid(),
                    src.Name,
                    src.Address,
                    new GeoPolygon(src.Polygon),
                    src.StartDate,
                    src.EndDate
                ));

            // UploadChecklistRequest → Checklist
            // ВАЖНО: FileId будет установлен позже, после загрузки файла в FileStorageService
            CreateMap<UploadChecklistRequest, Checklist>()
                .ConstructUsing(src => new Checklist(
                    Guid.NewGuid(),
                    src.ConstructionObjectId,
                    src.Type,
                    null, // будет заполнен после загрузки
                    src.Content
                ));

            // ActivateObjectRequest → Checklist (для акта открытия)
            // Аналогично: FileId будет известен только после загрузки
            CreateMap<ActivateObjectRequest, Checklist>()
                .ConstructUsing(src => new Checklist(
                    Guid.NewGuid(),
                    src.ObjectId,
                    ChecklistType.ActOpening,
                    null, // будет заполнен позже
                    src.ChecklistAnswers
                ));
        }
    }
}
