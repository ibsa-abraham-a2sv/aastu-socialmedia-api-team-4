using Application.Contracts;
using AutoMapper;
using FluentValidation;
using MediatR;

namespace Application.Features.Like.Commands.Delete_Like;

public class DeleteLikeCommandHandler : IRequestHandler<DeleteLikeCommand, bool>
{
    private readonly IPostRepository _postRepository;
    private readonly ILikeRepository _likeRepository;
    private readonly IMapper _mapper;

    public DeleteLikeCommandHandler(IPostRepository postRepository, ILikeRepository likeRepository, IMapper mapper)
    {
        _postRepository = postRepository;
        _likeRepository = likeRepository;
        _mapper = mapper;
    }
    
    public async Task<bool> Handle(DeleteLikeCommand request, CancellationToken cancellationToken)
    {
        var validator = new DeleteLikeCommandValidator(_postRepository);
        var validationResult = await validator.ValidateAsync(request, cancellationToken);
        
        if (!validationResult.IsValid)
            throw new ValidationException(validationResult.Errors);

        await _likeRepository.DeleteLikeByPostId(request.PostId, request.UserId);

        return true;
    }
}