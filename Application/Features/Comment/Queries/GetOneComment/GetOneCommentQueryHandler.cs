using Application.Contracts;
using Application.DTOs.Comment;
using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Comment.Queries.GetOneComment
{
    public class GetOneCommentQueryHandler : IRequestHandler<GetOneCommentQuery, CommentResponseDTO>
    {
        private readonly ICommentRepository _commentRepository;
        private readonly IMapper _mapper;

        public GetOneCommentQueryHandler(ICommentRepository commentRepository, IMapper mapper)
        {
            _commentRepository = commentRepository;
            _mapper = mapper;
        }
        public async Task<CommentResponseDTO> Handle(GetOneCommentQuery request, CancellationToken cancellationToken)
        {
            return _mapper.Map<CommentResponseDTO>(await _commentRepository.GetByIdAsync(request.Id));
        }  
    }
}
