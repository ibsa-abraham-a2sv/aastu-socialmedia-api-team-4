using Application.DTOs.Notification;
using Application.DTOs.User;
using Application.Features.Notification.Commands.ToggleNotification;
using Application.Features.Notification.Queries.GetNotifications;
using Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers;

[ApiController]
[Route("[controller]")]
public class NotificationController : ControllerBase
{
    private readonly IMediator _mediator;

    public NotificationController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost]
    public async Task<ActionResult<NotificationDto>> CreateNotification(NotificationDto notificationDto)
    {
        var command = new CreateNotificationCommand
        {
            NotificationDto = notificationDto
        };

        var notification = await _mediator.Send(command);

        return notification;
    }

    [HttpPost("toggle")]
    public async Task<ActionResult<NotificationDto>> ToggleNotification(NotificationDto notificationDto)
    {
        var command = new ToggleNotificationCommand
        {
            NotificationDto = notificationDto
        };

        var result = await _mediator.Send(command);

        return result;
    }

    [HttpGet]
    public async Task<ActionResult<List<NotificationDto>>> GetNotifications(int userId)
    {
        var command = new GetNotificationsOfUserCommand
        {
            UserId = userId
        };

        var res = await _mediator.Send(command);

        return res;
    }
}