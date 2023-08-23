using Application.Contracts;
using Application.DTOs.User;
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
        if (request.UserDto == null)
        {
            throw new Exception("empty in feature");
        }
        var user = _mapper.Map<UserEntity>(request.UserDto);
        user.PasswordHash = request.UserDto.Password;
        var res = await _userRepository.Register(user);
        return res.Id;
    }
}