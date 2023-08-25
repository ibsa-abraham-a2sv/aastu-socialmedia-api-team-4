using Application.DTOs.Tag;
using MediatR;

namespace Application.Features.Tag.Queries.GetTagById;

public class GetTagByIdQuery : IRequest<TagResponseDto>
{
    public int Id { get; set; }
}