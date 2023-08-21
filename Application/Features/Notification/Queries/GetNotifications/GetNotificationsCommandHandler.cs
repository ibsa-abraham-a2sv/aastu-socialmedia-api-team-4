using Application.Contracts;
using Application.DTOs.Notification;
using AutoMapper;
using MediatR;

namespace Application.Features.Notification.Queries.GetNotifications;

public class GetNotificationsCommandHandler : IRequestHandler<GetNotificationsCommand, List<NotificationDto>>
{
    private readonly INotificationRepository _notificationRepository;
    private readonly IUserRepository _userRepository;
    private readonly IMapper _mapper;

    public GetNotificationsCommandHandler(INotificationRepository notificationRepository, IUserRepository userRepository, IMapper mapper)
    {
        _notificationRepository = notificationRepository;
        _userRepository = userRepository;
        _mapper = mapper;
    }

    public async Task<List<NotificationDto>> Handle(GetNotificationsCommand request, CancellationToken cancellationToken)
    {
        // TODO: user UserValidator to check the user's existence

        var notifications = await _notificationRepository.GetNotificationsOfUser(request.UserRequestDto);

        return _mapper.Map<List<NotificationDto>>(notifications);
    }
}