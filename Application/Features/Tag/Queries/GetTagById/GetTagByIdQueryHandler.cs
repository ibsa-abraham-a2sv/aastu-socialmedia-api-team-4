using Application.Contracts;
using Application.DTOs.Tag;
using AutoMapper;
using MediatR;

namespace Application.Features.Tag.Queries.GetTagById;

public class GetTagByIdQueryHandler : IRequestHandler<GetTagByIdQuery, TagResponseDto>
{
    private readonly ITagRepository _tagRepository;
    private readonly IMapper _mapper;
    
    public GetTagByIdQueryHandler(ITagRepository tagRepository, IMapper mapper)
    {
        _tagRepository = tagRepository;
        _mapper = mapper;
    }
    
    public async Task<TagResponseDto> Handle(GetTagByIdQuery request, CancellationToken cancellationToken)
    {
        var tag = await _tagRepository.GetByIdAsync(request.Id);
        return _mapper.Map<TagResponseDto>(tag);
    }
}