using Application.Contracts;
using Application.Features.Notification.Queries.GetNotifications;
using Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.SignalR;

namespace WebApi.Service.NotificationService;

public class NotificationHub : Hub
{
    private readonly IUserConnectionRepository _userConnectionRepository;
    private readonly INotificationRepository _notificationRepository;
    private readonly IMediator _mediator;

    public NotificationHub(IUserConnectionRepository userConnectionRepository, INotificationRepository notificationRepository, IMediator mediator)
    {
        _userConnectionRepository = userConnectionRepository;
        _notificationRepository = notificationRepository;
        _mediator = mediator;
    }
    
    public override async Task OnConnectedAsync()
    {
        // get logged in user
        var loggedInUser = await AuthHelper.GetUserId(Context.User);
        
        // clean up
        await _userConnectionRepository.CleanUpMapping(loggedInUser);

        // map logged in user with connection id
        await _userConnectionRepository.CreateAsync(new UserConnectionEntity
        {
            UserId = loggedInUser,
            ConnectionId = Context.ConnectionId
        });

        var loggedInUserNotificationsList = await _mediator.Send(new GetNotificationsOfUserCommand
        {
            UserId = loggedInUser
        });

        Console.WriteLine($"Connection Id: {Context.ConnectionId}");
        var sendNotifications = new { unreadCount = loggedInUserNotificationsList.Count(n => !n.ReadStatus), notifications = loggedInUserNotificationsList };
        await Clients.Caller.SendAsync("ReceiveNotification", sendNotifications);
        // await Clients.All.SendAsync("ReceiveNotification", sendNotifications);
    }

    public override async Task OnDisconnectedAsync(Exception? exception)
    {
        var loggedInUser = await AuthHelper.GetUserId(Context.User);
        
        // clean up
        await _userConnectionRepository.CleanUpMapping(loggedInUser);
    }
}