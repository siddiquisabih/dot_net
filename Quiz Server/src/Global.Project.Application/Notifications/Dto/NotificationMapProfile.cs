using AutoMapper;
using Global.Project.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace Global.Project.Notifications.Dto
{
    public class NotificationMapProfile: Profile
    {
        public NotificationMapProfile()
        {
            CreateMap<NotificationDto, Notification>().ReverseMap();
        }
    }
}
