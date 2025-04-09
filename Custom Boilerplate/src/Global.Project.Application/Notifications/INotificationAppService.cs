using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Global.Project.DTOs;
using Global.Project.Notifications.Dto;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Global.Project.Notifications
{
    public interface INotificationAppService: IApplicationService
    {
        bool CreateNotification(NotificationDto notificationDto);
        bool UpdateNotification(NotificationDto notificationDto);
        NotificationDto GetNotificationById(int notificationId);
        Task<ListResultDto<NotificationDto>> GetNotifications();

        ListResultDto<NotificationGroupDto> GetNotificationGroups();
        Task SaveTaskGroup(NotificationGroupDto notificationGroupDto);
        NotificationGroupDto GetNotificationGroupById(int id);
        Task<bool> UpdateNotificationGroup(NotificationGroupDto notificationGroupDto);

    }
}
