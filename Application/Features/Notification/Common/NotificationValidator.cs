using Application.Contracts;
using Application.DTOs.Notification;
using FluentValidation;

namespace Application.Features.Notification.Commands.CreateNotification;

public class NotificationValidator : AbstractValidator<NotificationDto>
{
    private readonly IUserRepository _userRepository;

    public NotificationValidator(IUserRepository userRepository)
    {
        _userRepository = userRepository;

        RuleFor(n => n.UserId)
            .NotNull().WithMessage("{Property Name} should be provided.")
            .MustAsync(async (UserId, cancellationToken) =>
            {
                var userExists = await _userRepository.Exists(UserId);

                return userExists;
            })
            .WithMessage("{Property Name} doesn't exist.");

        RuleFor(n => n.Content)
            .NotNull().WithMessage("{Property Name} must not be null");

        RuleFor(n => n.ReadStatus)
            .Equal(false);
    }
}