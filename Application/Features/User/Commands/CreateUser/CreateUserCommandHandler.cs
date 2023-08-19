using Application.Contracts;
using AutoMapper;
using Domain.Entities;
using MediatR;

namespace Application.Features.User.Commands.CreateUser;

public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, int>
{
    private readonly IUserRepository _userRepository;
    private readonly IMapper _mapper;

    public CreateUserCommandHandler(IUserRepository userRepository, IMapper mapper)
    {
        _userRepository = userRepository;
        _mapper = mapper;
    }
    
    public async Task<int> Handle(CreateUserCommand request, CancellationToken cancellationToken)
    {
        var user = _mapper.Map<UserEntity>(request.UserDto);
        var res = await _userRepository.CreateAsync(user);
        return user.Id;
    }
}