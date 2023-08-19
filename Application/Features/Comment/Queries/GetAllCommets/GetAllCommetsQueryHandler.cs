using Application.Contracts;
using Application.DTOs.Comment;
using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Comment.Queries.GetAllCommets
{
    public class GetAllCommetsQueryHandler : IRequestHandler<GetAllCommentsQuery, List<CommentResponseDTO>>
    {
        private readonly ICommentRepository _commentRepository;
        private readonly IMapper _mapper;

        public GetAllCommetsQueryHandler(ICommentRepository commentRepository, IMapper mapper)
        {
            _commentRepository = commentRepository;
            _mapper = mapper;
        }

        public async Task<List<CommentResponseDTO>> Handle(GetAllCommentsQuery request, CancellationToken cancellationToken)
        {
            var comments = await _commentRepository.GetAllAsync();
            return _mapper.Map<List<CommentResponseDTO>>(comments);
        }
    }
}
