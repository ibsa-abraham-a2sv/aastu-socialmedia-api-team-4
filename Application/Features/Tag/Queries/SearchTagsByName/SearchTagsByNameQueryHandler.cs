using Application.Contracts;
using Application.DTOs.Tag;
using AutoMapper;
using MediatR;

namespace Application.Features.Tag.Queries.GetTagsByName;

public class SearchTagsByNameQueryHandler : IRequestHandler<SearchTagsByNameQuery, List<TagResponseDto>>
{
    private readonly ITagRepository _tagRepository;
    private readonly IMapper _mapper;
    
    public SearchTagsByNameQueryHandler(ITagRepository tagRepository, IMapper mapper)
    {
        _tagRepository = tagRepository;
        _mapper = mapper;
    }
    
    public async Task<List<TagResponseDto>> Handle(SearchTagsByNameQuery request, CancellationToken cancellationToken)
    {
        var tags = await _tagRepository.SearchTagsByName(request.Name);
        return _mapper.Map<List<TagResponseDto>>(tags);
    }
}