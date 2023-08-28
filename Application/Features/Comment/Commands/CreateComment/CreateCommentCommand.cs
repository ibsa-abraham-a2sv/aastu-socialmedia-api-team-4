using Application.DTOs.Comment;
using MediatR;

namespace Application.Features.Comment.Commands.CreateComment
{
    public class CreateCommentCommand : IRequest<CommentResponseDTO>
    {
        public int userId { get; set; }
        public CommentRequestDto commentRequestDTO { get; set; } = null!;
    }
}
