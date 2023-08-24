using Application.Contracts;
using Application.DTOs.Tag;
using AutoMapper;
using MediatR;

namespace Application.Features.Tag.Queries.GetAllTags;

public class GetAllTagsQueryHandler : IRequestHandler<GetAllTagsQuery, List<TagResponseDto>>
{
    private readonly ITagRepository _tagRepository;
    private readonly IMapper _mapper;
    
    public GetAllTagsQueryHandler(ITagRepository tagRepository, IMapper mapper)
    {
        _tagRepository = tagRepository;
        _mapper = mapper;
    }
    
    public async Task<List<TagResponseDto>> Handle(GetAllTagsQuery request, CancellationToken cancellationToken)
    {
        var tags = await _tagRepository.GetAllAsync();
        return _mapper.Map<List<TagResponseDto>>(tags);
    }
}