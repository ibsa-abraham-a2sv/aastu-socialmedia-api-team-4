using Application.Contracts;
using FluentValidation;

namespace Application.Features.Notification.Commands.ToggleNotification;

public class ToggleNotificationValidator : AbstractValidator<ToggleNotificationCommand>
{
    private readonly INotificationRepository _notificationRepository;

    public ToggleNotificationValidator(INotificationRepository notificationRepository)
    {
        _notificationRepository = notificationRepository;

        RuleFor(c => c.NotificationId)
            .MustAsync(async (notificationId, token) =>
            {
                var exists = await _notificationRepository.Exists(notificationId);

                return exists;
            });
    }
}