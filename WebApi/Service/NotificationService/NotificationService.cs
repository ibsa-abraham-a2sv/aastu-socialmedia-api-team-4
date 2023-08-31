using Application.Contracts;
using Application.Features.Notification.Queries.GetNotifications;
using Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.SignalR;

namespace WebApi.Service.NotificationService;

public class NotificationService : INotificationService
{
    private readonly IHubContext<NotificationHub> _hubContext;
    private readonly IUserRepository _userRepository;
    private readonly IUserConnectionRepository _userConnectionRepository;
    private readonly IPostRepository _postRepository;
    private readonly INotificationRepository _notificationRepository;
    private readonly IMediator _mediator;

    public NotificationService(IHubContext<NotificationHub> hubContext, IUserRepository userRepository,
        IUserConnectionRepository userConnectionRepository, IPostRepository postRepository, INotificationRepository notificationRepository, IMediator mediator)
    {
        _hubContext = hubContext;
        _userRepository = userRepository;
        _userConnectionRepository = userConnectionRepository;
        _postRepository = postRepository;
        _notificationRepository = notificationRepository;
        _mediator = mediator;
    }

    public async Task SendNotificationToSingleUser(int notificationReceiverUserId, string message)
    {
        // get receiver of notification connection id
        var receiverConnectionId = await _userConnectionRepository.GetUserConnection(notificationReceiverUserId);
        
        // add the notification to database
        await _notificationRepository.CreateAsync(new NotificationEntity
        {
            UserId = notificationReceiverUserId,
            Content = message,
            CreatedAt = DateTime.UtcNow,
            ModifiedAt = DateTime.UtcNow,
            ReadStatus = false
        });
        
        // send notifications list if notification receiver is logged in
        if (receiverConnectionId != null)
        {
            var notificationsList = await _mediator.Send(new GetNotificationsOfUserCommand
            {
                UserId = notificationReceiverUserId
            });
            
            // var notificationsList = await _notificationRepository.GetNotificationsOfUser(notificationReceiverUserId);
            var messages = new { unreadCount = notificationsList.Count(n => !n.ReadStatus), notifications = notificationsList };
            await _hubContext.Clients.Client(receiverConnectionId.ConnectionId).SendAsync("ReceiveNotification", messages);
        }
    }
    
    public async Task PostIsLiked(int postId, int postLiker)
    {
        var post = await _postRepository.GetByIdAsync(postId);
        var postLikerEntity = await _userRepository.GetByIdAsync(postLiker);

        var message = $"{postLikerEntity.UserName} liked your post titled \"{post.Title}\"";

        await SendNotificationToSingleUser(post.UserId, message);
    }

    public async Task UserIsFollowed(int followerUserId, int followedUserId)
    {
        var followerUserEntity = await _userRepository.GetByIdAsync(followerUserId);

        var message = $"{followerUserEntity.UserName} has followed you.";

        await SendNotificationToSingleUser(followedUserId, message);
    }
    
    public async Task PostIsCommentedOn(int postId, int commenterUserId)
    {
        var post = await _postRepository.GetByIdAsync(postId);
        var commenterUserEntity = await _userRepository.GetByIdAsync(commenterUserId);

        var message = $"{commenterUserEntity.UserName} commented on your post titled \"{post.Title}\"";

        await SendNotificationToSingleUser(post.UserId, message);
    }
}