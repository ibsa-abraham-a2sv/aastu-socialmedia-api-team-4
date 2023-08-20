using Application.Contracts;
using Application.DTOs.Follow;
using FluentValidation;

namespace Application.Features.Follow.Commands.DeleteFollow;

public class DeleteFollowCommandValidator : AbstractValidator<FollowDto>
{
    private readonly IFollowRepository _followRepository;

    public DeleteFollowCommandValidator(IFollowRepository followRepository)
    {
        _followRepository = followRepository;

        RuleFor(f => f.Id)
            .NotEmpty().WithMessage("{PropertyName} is required.")
            .NotNull()
            .MustAsync(async (Id, validationToken) =>
            {
                var followExists = await _followRepository.Exists(Id);

                return followExists;
            })
            .WithMessage("{PropertyName} does not exist.");
    }
}