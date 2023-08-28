using FluentValidation;

namespace Application.Features.Like.Commands.Create_Like;

public class CreateLikeCommandValidator : AbstractValidator<CreateLikeCommand>
{
    private readonly IPostRepository _postRepository;

    public CreateLikeCommandValidator(IPostRepository postRepository)
    {
        _postRepository = postRepository;
        RuleFor(x => x.LikeDto.PostId)
            .MustAsync(async (postId, token) => await _postRepository.Exists(postId));
    }
}