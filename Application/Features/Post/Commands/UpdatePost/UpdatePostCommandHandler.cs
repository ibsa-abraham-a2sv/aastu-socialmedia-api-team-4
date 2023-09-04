using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Contracts;
using Application.Contracts.Services;
using Application.Exceptions;
using AutoMapper;
using Domain.Entities;
using FluentValidation;
using MediatR;

namespace Application.Features.Post.Commands.UpdatePost;

public class UpdatePostCommandHandler : IRequestHandler<UpdatePostCommand,Unit>
{
    private readonly IMapper _mapper;
    private readonly IPostRepository _postRepository;
    private readonly IUserRepository _userRepository;
     private readonly IFileUploader _fileUploader;
    public UpdatePostCommandHandler(IFileUploader fileUploader,IPostRepository postRepository, IUserRepository userRepository, IMapper mapper)
    {
        _mapper = mapper;
        _postRepository = postRepository;
        _userRepository = userRepository;
        _fileUploader = fileUploader;
    }

    public async Task<Unit> Handle(UpdatePostCommand command, CancellationToken cancellationToken)
    {
        var validator = new UpdatePostCommandValidator(_userRepository);
        var validationResult = await validator.ValidateAsync(command);

        if (!validationResult.IsValid)
        {
            throw new ValidationException(validationResult.Errors);
        }
    
        var old_post = await _postRepository.GetByIdAsync(command.PostId);
        if (old_post == null)
        {
            throw new NotFoundException($"Post with id {command.PostId} does't exist!", command);
        }

        var post = _mapper.Map<PostEntity>(command.UpdatePost);
        post.UserId = command.UserId;

        if(command.UpdatePost.PictureFile != null){
            var user = await _userRepository.GetByIdAsync(command.UserId);
            var res = await _fileUploader.UploadImage(command.UpdatePost.PictureFile,$"{user.UserName}/post/");
             post.PicturePath = res.Url.ToString();
        }
        
        await _postRepository.UpdateAsync(command.PostId,post);
        return Unit.Value;
    }
}
