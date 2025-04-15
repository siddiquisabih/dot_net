using Abp.Application.Services;
using Global.Project.Hubs.NoficationHub.Dto;
using System.Threading.Tasks;

namespace Global.Project.Hubs.NoficationHub
{
    public interface ISignalRNotificationHub : IApplicationService
    {
        Task SendMessage(SignalRNotificationDto notificationDto);
    }
}
