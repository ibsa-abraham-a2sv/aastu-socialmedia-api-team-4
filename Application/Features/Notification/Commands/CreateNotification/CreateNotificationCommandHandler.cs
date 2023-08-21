using Application.Contracts;
using Application.DTOs.Notification;
using AutoMapper;
using Domain.Entities;
using FluentValidation;
using MediatR;

namespace Application.Features.Notification.Commands.CreateNotification;

public class CreateNotificationCommandHandler : IRequestHandler<CreateNotificationCommand, NotificationDto>
{
    private readonly INotificationRepository _notificationRepository;
    private readonly IUserRepository _userRepository;
    private readonly IMapper _mapper;

    public CreateNotificationCommandHandler(INotificationRepository notificationRepository, IUserRepository userRepository, IMapper mapper)
    {
        _notificationRepository = notificationRepository;
        _userRepository = userRepository;
        _mapper = mapper;
    }
    
    public async Task<NotificationDto> Handle(CreateNotificationCommand request, CancellationToken cancellationToken)
    {
        var validator = new CreateNotificationCommandValidator(_userRepository);
        var validationResult = validator.Validate(request.NotificationDto);

        if (!validationResult.IsValid)
            throw new ValidationException(validationResult.Errors);

        var notification = _mapper.Map<NotificationEntity>(request.NotificationDto);

        notification = await _notificationRepository.CreateAsync(notification);

        return _mapper.Map<NotificationDto>(notification);
    }
}