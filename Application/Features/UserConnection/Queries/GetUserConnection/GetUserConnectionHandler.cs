using Application.Contracts;
using Application.DTOs.UserConnection;
using Application.Features.UserConnection.Commands.DeleteUserConnection;
using AutoMapper;
using FluentValidation;
using MediatR;

namespace Application.Features.UserConnection.Queries.GetUserConnection;

public class GetUserConnectionHandler : IRequestHandler<GetUserConnection, UserConnectionDto>
{
    private readonly IUserConnectionRepository _userConnectionRepository;
    private readonly IMapper _mapper;

    public GetUserConnectionHandler(IUserConnectionRepository userConnectionRepository, IMapper mapper)
    {
        _userConnectionRepository = userConnectionRepository;
        _mapper = mapper;
    }
    
    public async Task<UserConnectionDto> Handle(GetUserConnection request, CancellationToken cancellationToken)
    {
        var validator = new GetUserConnectionValidator(_userConnectionRepository);
        var validationResult = await validator.ValidateAsync(request, cancellationToken);

        if (!validationResult.IsValid)
            throw new ValidationException(validationResult.Errors);

        var userConnection = await _userConnectionRepository.GetUserConnection(request.FindUserConnectionDto.UserId);

        return _mapper.Map<UserConnectionDto>(userConnection);
    }
}