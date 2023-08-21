using Application.Contracts;
using Application.DTOs.Notification;
using Application.Features.Notification.Commands.CreateNotification;
using AutoMapper;
using Domain.Entities;
using FluentValidation;
using MediatR;

namespace Application.Features.Notification.Commands.ToggleNotification;

public class ToggleNotificationCommandHandler : IRequestHandler<ToggleNotificationCommand, NotificationDto>
{
    private readonly INotificationRepository _notificationRepository;
    private readonly IUserRepository _userRepository;
    private readonly IMapper _mapper;

    public ToggleNotificationCommandHandler(INotificationRepository notificationRepository, IUserRepository userRepository, IMapper mapper)
    {
        _notificationRepository = notificationRepository;
        _userRepository = userRepository;
        _mapper = mapper;
    }
    
    public async Task<NotificationDto> Handle(ToggleNotificationCommand request, CancellationToken cancellationToken)
    {
        var validator = new NotificationValidator(_userRepository);
        var validationResults = validator.Validate(request.NotificationDto);

        if (!validationResults.IsValid)
            throw new ValidationException(validationResults.Errors);

        var notification = _mapper.Map<NotificationEntity>(request.NotificationDto);

        var tNotification = await _notificationRepository.ToggleNotification(notification);

        return _mapper.Map<NotificationDto>(tNotification);
    }
}