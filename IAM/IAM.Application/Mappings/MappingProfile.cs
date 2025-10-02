using AutoMapper;
using IAM.Application.DTOs.Requests;
using IAM.Application.DTOs.Responses;
using IAM.Application.Services;
using IAM.Domain.Entities;
using IAM.Domain.Enums;
using IAM.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace IAM.Application.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            // === Domain → Response DTOs ===

            CreateMap<User, UserResponseDto>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id.ToString()))
            .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.LoginInfo.Username))
            .ForMember(dest => dest.FullName, opt => opt.MapFrom(src => src.FIO.FullName))
            .ForMember(dest => dest.Initials, opt => opt.MapFrom(src => src.FIO.Initials))
            .ForMember(dest => dest.Contacts, opt => opt.MapFrom(src => new ContactsDTO
            {
                Email = src.LoginInfo.Email,
                Phone = src.LoginInfo.PhoneNumber,
                WhatsApp = src.Messengers.WhatsApp,
                VK = src.Messengers.VK,
                Max = src.Messengers.Max,
                Telegram = src.Messengers.Telegram,
                Other = src.Messengers.Other
            }));

            // === Request DTOs → Domain Entities ===

            /*CreateMap<RegisterRequestDto, User>()
                .ConstructUsing(src =>
                    new User(
                        Guid.NewGuid(),
                        new(src.UserName, src.Contacts.Email, src.Contacts.Phone),
                        PasswordHasherService.HashPassword(src.Password),
                        new(src.Name, src.Sername, src.Fathername),
                        new(src.Contacts.WhatsApp, src.Contacts.VK, src.Contacts.Max, src.Contacts.Telegram, src.Contacts.Other),
                        (Roles)Enum.Parse(typeof(Roles), src.Role!)
                    ));*/

            

        }
    }
}
