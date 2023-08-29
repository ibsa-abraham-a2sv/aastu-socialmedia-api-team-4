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
        var validator = new ToggleNotificationValidator(_notificationRepository);
        var validationResults = await validator.ValidateAsync(request, cancellationToken);

        if (!validationResults.IsValid)
            throw new ValidationException(validationResults.Errors);

        var tNotification = await _notificationRepository.ToggleNotification(request.NotificationId);

        return _mapper.Map<NotificationDto>(tNotification);
    }
}