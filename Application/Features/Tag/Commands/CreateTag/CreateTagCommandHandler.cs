using Application.Contracts;
using Application.DTOs.Tag;
using AutoMapper;
using Domain.Entities;
using FluentValidation;
using MediatR;

namespace Application.Features.Tag.Commands.CreateTag;

public class CreateTagCommandHandler : IRequestHandler<CreateTagCommand, TagResponseDto>
{
    private readonly ITagRepository _tagRepository;
    private readonly IMapper _mapper;
    
    public CreateTagCommandHandler(ITagRepository tagRepository, IMapper mapper)
    {
        _tagRepository = tagRepository;
        _mapper = mapper;
    }
    
    public async Task<TagResponseDto> Handle(CreateTagCommand request, CancellationToken cancellationToken)
    {
        var validator = new CreateTagCommandValidator();
        var validationResult = await validator.ValidateAsync(request);
        
        if (!validationResult.IsValid)
        {
            throw new ValidationException(validationResult.Errors);
        }
        
        var tag = _mapper.Map<TagEntity>(request.TagRequestDto);
        var res = await _tagRepository.CreateAsync(tag);

        return _mapper.Map<TagResponseDto>(res);
    }
}