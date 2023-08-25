using Application.Contracts;
using Application.Contracts.Infrastructure;
using Application.DTOs.User;
using AutoMapper;
using Domain.Entities;
using Domain.Entities.Email;
using MediatR;

namespace Application.Features.User.Commands.CreateUser;

public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, int>
{
    private readonly IUserRepository _userRepository;
    private readonly IMapper _mapper;
    private readonly IEmailSender _emailSender;

    public CreateUserCommandHandler(IUserRepository userRepository, IEmailSender emailSender, IMapper mapper)
    {
        _userRepository = userRepository;
        _mapper = mapper;
        _emailSender = emailSender;
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
        await _emailSender.SendEmail(new Email()
        {
            To = request.UserDto.Email
        });
        return res.Id;
    }
}