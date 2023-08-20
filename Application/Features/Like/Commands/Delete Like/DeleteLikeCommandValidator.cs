using Application.Contracts;
using FluentValidation;

namespace Application.Features.Like.Commands.Delete_Like;

public class DeleteLikeCommandValidator : AbstractValidator<DeleteLikeCommand>
{
    private readonly ILikeRepository _likeRepository;

    public DeleteLikeCommandValidator(ILikeRepository likeRepository)
    {
        _likeRepository = likeRepository;
        
        RuleFor(x => x.Id)
            .NotEmpty()
            .WithMessage("{PropertyName} is required")
            .GreaterThan(0)
            .WithMessage("{PropertyName} can't be less than 1");
        
        RuleFor(c => c.Id)
            .MustAsync(async (id, cancellationToken) =>
            {
                var likeExists = await _likeRepository.Exists(id);

                return !likeExists;
            })
            .WithMessage("{PropertyName} does not exist.");
    }
}