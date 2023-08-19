using Application.DTOs.Comment;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Comment.Commands.UpdateComment
{
    public class UpdateCommentCommand : IRequest<Unit>
    {
        public CommentRequestDTO? UpdateCommentDTO{ get; set; }
    }
}
