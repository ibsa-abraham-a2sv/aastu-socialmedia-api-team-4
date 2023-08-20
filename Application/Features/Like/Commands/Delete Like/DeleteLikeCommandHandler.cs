using Application.Contracts;
using AutoMapper;
using FluentValidation;
using MediatR;

namespace Application.Features.Like.Commands.Delete_Like;

public class DeleteLikeCommandHandler : IRequestHandler<DeleteLikeCommand, bool>
{
    private readonly ILikeRepository _likeRepository;
    private readonly IMapper _mapper;

    public DeleteLikeCommandHandler(ILikeRepository likeRepository, IMapper mapper)
    {
        _likeRepository = likeRepository;
        _mapper = mapper;
    }
    
    public async Task<bool> Handle(DeleteLikeCommand request, CancellationToken cancellationToken)
    {
        var validator = new DeleteLikeCommandValidator(_likeRepository);
        var validationResult = await validator.ValidateAsync(request, cancellationToken);
        
        if (!validationResult.IsValid)
            throw new ValidationException(validationResult.Errors);

        await _likeRepository.DeleteAsync(request.Id);

        return true;
    }
}