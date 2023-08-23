using Application.Contracts;
using Application.DTOs.Comment;
using AutoMapper;
using FluentValidation;
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
            var validator = new GetOneCommentQueryValidator();
            var validationResult = await validator.ValidateAsync(request);

            if (!validationResult.IsValid)
            {
                throw new ValidationException(validationResult.Errors);
            }

            var comment = await _commentRepository.GetByIdAsync(request.Id);
            if (comment == null)
            { 
                throw new Exception("Comment with id " + request.Id + " not found"); 
            }

            return _mapper.Map<CommentResponseDTO>(comment);
        }  
    }
}
