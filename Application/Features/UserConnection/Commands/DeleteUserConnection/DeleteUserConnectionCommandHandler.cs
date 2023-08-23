using Application.Contracts;
using AutoMapper;
using Domain.Entities;
using FluentValidation;
using MediatR;

namespace Application.Features.UserConnection.Commands.DeleteUserConnection;

public class DeleteUserConnectionCommandHandler : IRequestHandler<DeleteUserConnectionCommand, Unit>
{
    private readonly IUserConnectionRepository _userConnectionRepository;
    private readonly IMapper _mapper;

    public DeleteUserConnectionCommandHandler(IUserConnectionRepository userConnectionRepository, IMapper mapper)
    {
        _userConnectionRepository = userConnectionRepository;
        _mapper = mapper;
    }
    
    public async Task<Unit> Handle(DeleteUserConnectionCommand request, CancellationToken cancellationToken)
    {
        var validator = new DeleteUserConnectionValidator(_userConnectionRepository);
        var validationResult = validator.Validate(request);

        if (!validationResult.IsValid)
            throw new ValidationException(validationResult.Errors);
        
        var userConnection = _mapper.Map<UserConnectionEntity>(request.FindUserConnectionDto);

        await _userConnectionRepository.DeleteAsync(userConnection.UserId);
        
        return Unit.Value;
    }
}