using Application.Contracts;
using Application.DTOs.Like;
using AutoMapper;
using Domain.Entities;
using FluentValidation;
using MediatR;

namespace Application.Features.Like.Commands.Create_Like;

public class CreateLikeCommandHandler : IRequestHandler<CreateLikeCommand, LikeDto>
{
    private readonly ILikeRepository _likeRepository;
    private readonly IPostRepository _postRepository;
    private readonly IMapper _mapper;

    public CreateLikeCommandHandler(ILikeRepository likeRepository, IPostRepository postRepository, IMapper mapper)
    {
        _likeRepository = likeRepository;
        _postRepository = postRepository;
        _mapper = mapper;
    }


    public async Task<LikeDto> Handle(CreateLikeCommand request, CancellationToken cancellationToken)
    {
        var validator = new CreateLikeCommandValidator(_postRepository);
        var validationResult = await validator.ValidateAsync(request, cancellationToken);

        if (!validationResult.IsValid)
        {
            throw new ValidationException(validationResult.Errors);
        }
        
        var like = _mapper.Map<LikeEntity>(request.LikeDto);
        ;
        like = await _likeRepository.CreateLike(like);

        return _mapper.Map<LikeDto>(like);
    }
}