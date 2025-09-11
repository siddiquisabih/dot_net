using Abp.Application.Services.Dto;
using Abp.Domain.Repositories;
using Abp.Runtime.Session;
using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Linq;
using Abp.ObjectMapping;
using Abp.Domain.Uow;
using System.Transactions;
using Microsoft.AspNetCore.SignalR;
using Microsoft.AspNetCore.Mvc;
using Global.Project.DTOs;
using Global.Project.Authorization.Users;
using Global.Project.Model;
using Global.Project.Hubs.NoficationHub;
using Global.Project.Notifications.Dto;

namespace Global.Project.Notifications
{
    [Authorize]
    public class NotificationAppService : INotificationAppService
    {
        private readonly IRepository<Notification> _notificationRepository;
        private readonly IAbpSession _abpSession;
        private readonly IObjectMapper _objectMapper;

        private readonly IUnitOfWorkManager _unitOfWorkManager;
        private readonly IRepository<UserSignalrConnection> _userSignalrConnectionRepository;
        private readonly IRepository<NotificationGroup> _notificationGroupRepository;
        private readonly IRepository<UserNotificationGroup> _userNotificationGroupRepository;

        private readonly IRepository<User, long> _userRepository;
        private IHubContext<SignalRNotificationHub> _taskHub;

        public NotificationAppService(IRepository<Notification> notificationRepository,
            IAbpSession abpSession,
            IObjectMapper objectMapper,
            IUnitOfWorkManager unitOfWorkManager,
            IRepository<UserSignalrConnection> userSignalrConnectionRepository,
            IRepository<NotificationGroup> notificationGroupRepository,
            IRepository<User, long> userRepository,
            IHubContext<SignalRNotificationHub> taskHub,
            IRepository<UserNotificationGroup> userNotificationGroupRepository)
        {
            _notificationRepository = notificationRepository;
            _abpSession = abpSession;
            _objectMapper = objectMapper;
            _unitOfWorkManager = unitOfWorkManager;
            _userSignalrConnectionRepository = userSignalrConnectionRepository;
            _notificationGroupRepository = notificationGroupRepository;
            _userRepository = userRepository;
            _taskHub = taskHub;
            _userNotificationGroupRepository = userNotificationGroupRepository;
        }

        public bool CreateNotification(NotificationDto notificationDto)
        {
            return _notificationRepository.InsertAndGetId(_objectMapper.Map<Notification>(notificationDto)) > 0;
        }

        public NotificationDto GetNotificationById(int notificationId)
        {
            var entity = _notificationRepository.Get(notificationId);
            return _objectMapper.Map<NotificationDto>(entity);
        }

        public async Task<ListResultDto<NotificationDto>> GetNotifications()
        {
            try
            {
                var no = _notificationRepository.GetAll();
                var notifications = await _notificationRepository.GetAllListAsync(n => n.UserId == _abpSession.UserId && !n.IsRead);
                if (notifications != null)
                {
                    notifications=notifications.OrderByDescending(n => n.Id).ToList();
                    var notificationDTOs = _objectMapper.Map<List<NotificationDto>>(notifications);

                    return new ListResultDto<NotificationDto>(notificationDTOs);
                }
                return new ListResultDto<NotificationDto>();
            }
            catch (Exception ex)
            {

                throw;
            }
        }
        [HttpPost]
        public bool UpdateNotification(NotificationDto notificationDto)
        {
            try
            {
                var notification = _notificationRepository.Get(notificationDto.Id);
                if (notification != null)
                {
                    notification.IsRead = true;
                    return _notificationRepository.Update(notification) != null;
                }

                return false;

            }
            catch (Exception ex)
            {

                throw;
            }
        }

        public int GetUnseenNotificationCount()
        {
            try
            {
                var count = _notificationRepository.Count(n => n.UserId == _abpSession.UserId && !n.IsRead);
                return count;
            }
            catch (Exception ex)
            {

                throw;
            }
        }


        public async Task SaveTaskGroup(NotificationGroupDto notificationGroupDto)
        {

            using (var unitOfWork = _unitOfWorkManager.Begin(TransactionScopeOption.Suppress))
            {
                var userConnections = _userSignalrConnectionRepository.GetAllList(us => notificationGroupDto.AssigneeIds.Contains(Convert.ToInt32(us.UserId))).Distinct().ToList();

                var NotificationGroupEntity = new NotificationGroup()
                {
                    GroupName = notificationGroupDto?.GroupName,
                    Description = notificationGroupDto?.Description,
                };

                foreach (var AssigneeId in notificationGroupDto.AssigneeIds)
                {
                    var userEntity = _userRepository.Get(AssigneeId);

                    var UserNotificationGroupEntity = new UserNotificationGroup()
                    {
                        User = userEntity,
                        NotificationGroup = NotificationGroupEntity
                    };

                    NotificationGroupEntity.UserNotificationGroups.Add(UserNotificationGroupEntity);
                    await _notificationGroupRepository.InsertAsync(NotificationGroupEntity);
                }

                unitOfWork.Complete();
            }
        }


        public ListResultDto<NotificationGroupDto> GetNotificationGroups()
        {
            try
            {
                var notificationGroups = _notificationGroupRepository.GetAll().OrderByDescending(x=>x.Id).Select(x => new NotificationGroupDto
                {
                    Id = x.Id,
                    GroupName = x.GroupName,
                    Description = x.Description
                }).ToList();

                return new ListResultDto<NotificationGroupDto>(notificationGroups);
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        public NotificationGroupDto GetNotificationGroupById(int id)
        {
            try
            {
                using (var unitOfWork = _unitOfWorkManager.Begin(TransactionScopeOption.Suppress))
                {
                    var notificationGroupDto = _notificationGroupRepository.GetAllIncluding(x => x.UserNotificationGroups)
                                .Where(x => x.Id == id).Select(x => new NotificationGroupDto
                                {
                                    Id = x.Id,
                                    GroupName = x.GroupName,
                                    Description = x.Description,
                                    AssigneeIds = x.UserNotificationGroups.Select(y => y.UserId).ToList()
                                }).SingleOrDefault();

                    return notificationGroupDto;
                }

            }
            catch (Exception ex)
            {

                throw;
            }

        }

        [HttpPost]
        public async Task<bool> UpdateNotificationGroup(NotificationGroupDto notificationGroupDto)
        {
            var response = false;
            if (notificationGroupDto != null)
            {

                using (var unitOfWork = _unitOfWorkManager.Begin(TransactionScopeOption.Suppress))
                {
                    var AlreadyExistUserNotificationGroupEntityList = _userNotificationGroupRepository
                        .GetAllList(x => x.NotificationGroupId == notificationGroupDto.Id);
                    
                  
                    foreach (var AlreadyExistUserNotificationGroupEntity in AlreadyExistUserNotificationGroupEntityList)
                    {
                        _userNotificationGroupRepository.Delete(AlreadyExistUserNotificationGroupEntity);
                    }

                    var NotificationGroupEntity = new NotificationGroup()
                    {
                        Id = notificationGroupDto.Id,
                        GroupName = notificationGroupDto?.GroupName,
                        Description = notificationGroupDto?.Description,
                    };

                    foreach (var AssigneeId in notificationGroupDto.AssigneeIds)
                    {
                        var userEntity = _userRepository.Get(AssigneeId);

                        var UserNotificationGroupEntity = new UserNotificationGroup()
                        {
                            User = userEntity,
                            NotificationGroup = NotificationGroupEntity
                        };

                        NotificationGroupEntity.UserNotificationGroups.Add(UserNotificationGroupEntity);
                        _notificationGroupRepository.InsertOrUpdate(NotificationGroupEntity);
                    }

                    unitOfWork.Complete();
                }

                response = true;
            }
            return response;
        }


        [HttpGet]
        public async Task<bool> DeleteNotificationGroup(int groupTaskId)
        {
            try
            {
               await _notificationGroupRepository.DeleteAsync(groupTaskId);
                return true;
            }

            catch (Exception ex)
            {

                throw;
            }
        }


    }
}
