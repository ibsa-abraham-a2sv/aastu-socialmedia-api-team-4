using Application.DTOs.Comment;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Comment.Commands.DeleteComment
{
    public class CreateCommentCommand : IRequest<CommentResponseDTO>
    {
        public CommentRequestDTO? commentRequestDTO { get; set; }
    }
}
