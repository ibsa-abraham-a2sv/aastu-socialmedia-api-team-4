using Application.DTOs.Tag;
using MediatR;

namespace Application.Features.Tag.Commands.CreateTag;

public class CreateTagCommand : IRequest<TagResponseDto>
{
    public TagRequestDto TagRequestDto { get; set; } = null!;
}