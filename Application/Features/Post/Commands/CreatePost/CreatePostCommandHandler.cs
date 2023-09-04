using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Contracts;
using Application.Contracts.Services;
using Application.DTOs.Post;
using Application.Exceptions;
using AutoMapper;
using Domain.Entities;
using FluentValidation;
using MediatR;

namespace Application.Features.Post.Commands.CreatePost;

public class CreatePostCommandHandler : IRequestHandler<CreatePostCommand,PostResponseDto>
{
    private readonly IMapper _mapper;
    private readonly IPostRepository _postRepository;
    private readonly IUserRepository _userRepository;
    private readonly ITagRepository _tagRepository;
    private readonly IFileUploader _fileUploader;
    
    public CreatePostCommandHandler(IFileUploader fileUploader, IPostRepository postRepository, IUserRepository userRepository, ITagRepository tagRepository, IMapper mapper)
    {
        _mapper = mapper;
        _postRepository = postRepository;
        _userRepository = userRepository;
        _tagRepository = tagRepository;
        _fileUploader = fileUploader;
    }

    public async Task<PostResponseDto> Handle(CreatePostCommand command, CancellationToken cancellationToken)
    {
        var validator = new CreatePostCommandValidator(_userRepository);
        var validationResult = await validator.ValidateAsync(command, cancellationToken);
        if (!validationResult.IsValid)
        {
            throw new ValidationException(validationResult.Errors);
        }

        var post = _mapper.Map<PostEntity>(command.NewPost);
        post.UserId = command.UserId;

        if(command.NewPost.PictureFile != null){

            var user = await _userRepository.GetByIdAsync(command.UserId);
            var res = await _fileUploader.UploadImage(command.NewPost.PictureFile,$"{user.UserName}/post/");
             post.PicturePath = res.Url.ToString();
        }
        var createdPost = await _postRepository.CreateAsync(await CreateTagsAndReturn(post));

        return _mapper.Map<PostResponseDto>(createdPost);
    }

    private async Task<PostEntity> CreateTagsAndReturn(PostEntity entity)
    {
        var post = entity ;
        var words = post.Content.Split(new[] { ' ', '\t', '\r', '\n', ',' },
            StringSplitOptions.RemoveEmptyEntries);
        post.Tags = new List<TagEntity>();
        foreach (var word in words)
        {
            if (word.StartsWith("#"))
            {
                var tagName = word.Trim('#');
                var existingTag = await _tagRepository.GetTagByName(tagName);
                if (existingTag == null)
                {
                    existingTag = new TagEntity()
                    {
                        Name = tagName
                    };
                    await _tagRepository.CreateAsync(existingTag);
                }
                post.Tags.Add(existingTag);
            }
        }

        return post;
    }
}
