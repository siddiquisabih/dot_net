using Abp.Runtime.Session;
using Microsoft.AspNetCore.SignalR;
using System.Threading.Tasks;
using System;
using Abp.Dependency;
using Abp.Domain.Repositories;
using System.Linq;
using Global.Project.Model;

namespace Global.Project.Hubs.NoficationHub
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
