using Application.DTOs.Tag;
using MediatR;

namespace Application.Features.Tag.Queries.GetTagsByName;

public class SearchTagsByNameQuery : IRequest<List<TagResponseDto>>
{
    public string Name { get; set; } = null!;
}