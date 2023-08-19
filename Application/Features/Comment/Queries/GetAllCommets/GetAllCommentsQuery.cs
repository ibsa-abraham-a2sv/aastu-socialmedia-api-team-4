using Application.DTOs.Comment;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Comment.Queries.GetAllCommets
{
    public class GetAllCommentsQuery : IRequest<List<CommentResponseDTO>>
    {
    }
}
