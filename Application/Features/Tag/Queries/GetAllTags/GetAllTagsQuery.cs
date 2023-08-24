using Application.DTOs.Tag;
using MediatR;

namespace Application.Features.Tag.Queries.GetAllTags;

public class GetAllTagsQuery : IRequest<List<TagResponseDto>>
{
    
}