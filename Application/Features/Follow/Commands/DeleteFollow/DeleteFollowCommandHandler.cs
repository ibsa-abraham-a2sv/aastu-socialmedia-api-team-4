using Application.Contracts;
using Application.DTOs.Follow;
using AutoMapper;
using Domain.Entities;
using FluentValidation;
using MediatR;

namespace Application.Features.Follow.Commands.DeleteFollow;

public class DeleteFollowCommandHandler : IRequestHandler<DeleteFollowCommand, bool>
{
    private readonly IFollowRepository _followRepository;
    private readonly IMapper _mapper;

    public DeleteFollowCommandHandler(IFollowRepository followRepository, IMapper mapper)
    {
        _followRepository = followRepository;
        _mapper = mapper;
    }
    
    public async Task<bool> Handle(DeleteFollowCommand request, CancellationToken cancellationToken)
    {
        var validator = new DeleteFollowCommandValidator(_followRepository);
        var validationResult = validator.Validate(request.FollowDto);
        
        if (!validationResult.IsValid)
            throw new ValidationException(validationResult.Errors);

        var follow = _mapper.Map<FollowEntity>(request.FollowDto);

        await _followRepository.DeleteAsync(follow.Id);

        return true;
    }
}