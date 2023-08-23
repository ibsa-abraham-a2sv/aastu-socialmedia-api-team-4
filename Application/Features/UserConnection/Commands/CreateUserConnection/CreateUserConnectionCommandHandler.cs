using Application.Contracts;
using Application.DTOs.UserConnection;
using AutoMapper;
using Domain.Entities;
using FluentValidation;
using MediatR;

namespace Application.Features.UserConnection.Commands;

public class CreateUserConnectionCommandHandler : IRequestHandler<CreateUserConnectionCommand, UserConnectionDto>
{
    private readonly IUserConnectionRepository _userConnectionRepository;
    private readonly IUserRepository _userRepository;
    private readonly IMapper _mapper;

    public CreateUserConnectionCommandHandler(IUserConnectionRepository userConnectionRepository, IUserRepository userRepository, IMapper mapper)
    {
        _userConnectionRepository = userConnectionRepository;
        _userRepository = userRepository;
        _mapper = mapper;
    }
    
    public async Task<UserConnectionDto> Handle(CreateUserConnectionCommand request, CancellationToken cancellationToken)
    {
        var validator = new CreateUserConnectionCommandValidator(_userRepository);
        var validationResult = validator.Validate(request);

        if (!validationResult.IsValid)
            throw new ValidationException(validationResult.Errors);

        var newUserConnectionEntity = _mapper.Map<UserConnectionEntity>(request.CreateUserConnectionDto);
        var userConnection = await _userConnectionRepository.CreateAsync(newUserConnectionEntity);

        return _mapper.Map<UserConnectionDto>(userConnection);
    }
}