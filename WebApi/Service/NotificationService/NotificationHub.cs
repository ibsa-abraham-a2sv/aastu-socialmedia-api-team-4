using Application.Contracts;
using Domain.Entities;
using Microsoft.AspNetCore.SignalR;

namespace WebApi.Service.NotificationService;

public class NotificationHub : Hub
{
    private readonly IUserConnectionRepository _userConnectionRepository;
    private readonly INotificationRepository _notificationRepository;

    public NotificationHub(IUserConnectionRepository userConnectionRepository, INotificationRepository notificationRepository)
    {
        _userConnectionRepository = userConnectionRepository;
        _notificationRepository = notificationRepository;
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

        var loggedInUserNotificationsList = await _notificationRepository.GetNotificationsOfUser(loggedInUser);

        Console.WriteLine($"Connection Id: {Context.ConnectionId}");
        await Clients.All.SendAsync("ReceiveNotification", new {notifications = loggedInUserNotificationsList});
    }

    public override async Task OnDisconnectedAsync(Exception? exception)
    {
        var loggedInUser = await AuthHelper.GetUserId(Context.User);
        
        // clean up
        await _userConnectionRepository.CleanUpMapping(loggedInUser);
    }
}