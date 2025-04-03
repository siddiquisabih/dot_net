using Abp.Application.Services;
using System.Threading.Tasks;

namespace Tatweer.SafeCity.Core
{
    public interface ISignalRNotificationHub : IApplicationService
    {
        Task SendMessage(SignalRNotificationDto notificationDto);
    }
}
