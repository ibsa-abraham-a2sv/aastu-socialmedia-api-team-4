using Application.DTOs.Comment;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Comment.Queries.GetOneComment
{
    public class GetOneCommentQuery : IRequest<CommentResponseDTO>
    {
        public int Id { get; set; }
    }
}
