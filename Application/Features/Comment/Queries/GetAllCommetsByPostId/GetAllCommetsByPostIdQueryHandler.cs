using Application.Contracts;
using Application.DTOs.Comment;
using Application.Features.Comment.Queries.GetAllCommetsGetAllCommetsByPostId;
using AutoMapper;
using FluentValidation;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Comment.Queries.GetAllCommets.GetAllCommetsByPostId
{
    public class GetAllCommetsByPostIdQueryHandler : IRequestHandler<GetAllCommentsByPostIdQuery, List<CommentResponseDTO>>
    {
        private readonly ICommentRepository _commentRepository;
        private readonly IMapper _mapper;

        public GetAllCommetsByPostIdQueryHandler(ICommentRepository commentRepository, IMapper mapper)
        {
            _commentRepository = commentRepository;
            _mapper = mapper;
        }

        public async Task<List<CommentResponseDTO>> Handle(GetAllCommentsByPostIdQuery request, CancellationToken cancellationToken)
        {
            var validator = new GetCommentByPostIdQueryValidator();
            var validationResult = validator.Validate(request);

            if (!validationResult.IsValid)
            {
                throw new ValidationException(validationResult.Errors);
            }

            var comments = await _commentRepository.GetCommentByPostId(request.PostId);
            return _mapper.Map<List<CommentResponseDTO>>(comments);
        }
    }
}
