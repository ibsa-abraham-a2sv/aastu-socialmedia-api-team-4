using Application.Contracts;
using FluentValidation;

namespace Application.Features.Like.Commands.Delete_Like;

public class DeleteLikeCommandValidator : AbstractValidator<DeleteLikeCommand>
{
    public DeleteLikeCommandValidator(IPostRepository postRepository)
    {
        RuleFor(c => c.PostId)
            .MustAsync(async (id, cancellationToken) =>
            {
                var likeExists = await postRepository.Exists(id);

                return likeExists;
            })
            .WithMessage("{PropertyName} does not exist.");
    }
}