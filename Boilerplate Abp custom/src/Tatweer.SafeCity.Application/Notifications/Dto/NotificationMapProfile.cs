using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;
using Tatweer.SafeCity.Model;

namespace Tatweer.SafeCity.Notifications.Dto
{
    public class NotificationMapProfile: Profile
    {
        public NotificationMapProfile()
        {
            CreateMap<NotificationDto, Notification>().ReverseMap();
        }
    }
}
