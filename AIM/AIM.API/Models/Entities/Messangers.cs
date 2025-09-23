using Microsoft.AspNetCore.SignalR;
using System.ComponentModel.DataAnnotations;

namespace AIM.API.Models.Entities
{
    /// <summary>
    /// Класс хранящий в себе ссылки? имена пользователей в качестве контактной информации
    /// </summary>
    public class Messangers(Guid userId, string? whatsApp, string? vK, string? max, string? telegram, string? other)
    {

        /// <summary>
        /// Id пользователя (внешний ключ)
        /// </summary>
        public Guid UserId { get; set; } = userId;

        public string? WhatsApp { get; set; } = whatsApp;

        public string? VK { get; set; } = vK;

        public string? Max { get; set; } = max;

        public string? Telegram { get; set; } = telegram;

        public string? Other { get; set; } = other;
    }


}
