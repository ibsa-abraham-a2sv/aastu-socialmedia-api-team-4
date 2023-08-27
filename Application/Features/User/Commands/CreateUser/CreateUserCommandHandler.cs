using Application.Contracts;
using Application.Contracts.Infrastructure;
using Application.DTOs.User;
using AutoMapper;
using Domain.Entities;
using FluentValidation;
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
        var validator = new CreateUserCommandValidator();
        var validationResult = await validator.ValidateAsync(request, cancellationToken);
        if (!validationResult.IsValid)
        {
            throw new ValidationException(validationResult.Errors);
        }   
        
        // hash password
        request.UserDto.Password = BCrypt.Net.BCrypt.HashPassword(request.UserDto.Password);
        var user = _mapper.Map<UserEntity>(request.UserDto);
        var res = await _userRepository.CreateAsync(user);

//         var res = await _userRepository.Register(user);
        await _emailSender.SendEmail(new Email()
        {
            To = request.UserDto.Email
        });
        return res.Id;
    }
}