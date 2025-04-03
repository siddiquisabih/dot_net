using Abp.Runtime.Session;
using Microsoft.AspNetCore.SignalR;
using System.Threading.Tasks;
using System;
using Abp.Dependency;
using Abp.Domain.Repositories;
using Tatweer.SafeCity.Model;
using System.Linq;

namespace Tatweer.SafeCity.Core
{
    public class SignalRNotificationHub : Hub, ITransientDependency
    {
        private readonly IAbpSession _abpSession;
        private readonly IRepository<UserSignalrConnection> _userSignalrConnectionRepository;
        public SignalRNotificationHub(IRepository<UserSignalrConnection> userSignalrConnectionRepository,
            IAbpSession abpSession)
        {
            _userSignalrConnectionRepository = userSignalrConnectionRepository;
            _abpSession = abpSession;
        }

        public SignalRNotificationHub()
        {

        }
        //public async Task SendMessage(SignalRNotificationDto notificationDto)
        //{
        //    await Clients.All.SendAsync("ReceiveMessage", _abpSession.UserId, "New Task Created");
        //    await Clients.Client(connectionId).SendAsync("ReceiveMessage", _abpSession.UserId, "New Task Created");
        //}
        public override Task OnConnectedAsync()
        {
            if (_abpSession.UserId.HasValue)
            {
                var platform = (string)Context.GetHttpContext().Request.Query["platform"];
                
                var connectionId = Context.ConnectionId;

                    _userSignalrConnectionRepository.InsertAsync(new UserSignalrConnection()
                    {
                        ConnectionId = connectionId,
                        UserId = _abpSession.UserId.Value,
                        Platform = platform
                    });

                return base.OnConnectedAsync();
            }
            //return base.OnConnectedAsync();
            return null;
        }

        public override Task OnDisconnectedAsync(Exception exception)
        {
            var platform = (string)Context.GetHttpContext().Request.Query["platform"];
            if (_abpSession.UserId.HasValue && platform !=null)
            {
              var  _SignalRUser= _userSignalrConnectionRepository.GetAll().Where(us => us.UserId == _abpSession.UserId && us.Platform == platform).FirstOrDefault();
                if (_SignalRUser != null)
                {
                    _userSignalrConnectionRepository.Delete(us => us.UserId == _abpSession.UserId && us.Platform == platform);
                }
                       
            }
            return base.OnDisconnectedAsync(exception);
        }
    }
}
