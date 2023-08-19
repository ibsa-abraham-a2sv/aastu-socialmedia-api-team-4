using Application.Contracts;
using Application.DTOs.Comment;
using AutoMapper;
using Domain.Entities;
using FluentValidation;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Comment.Commands.DeleteComment
{
    public class CreateCommentCommandHandler : IRequestHandler<CreateCommentCommand, CommentResponseDTO>
    {
        private readonly ICommentRepository _commentRepository;
        private readonly IMapper _mapper;
        public CreateCommentCommandHandler(ICommentRepository commentRepository, IMapper mapper)
        {
            _commentRepository = commentRepository;
            _mapper = mapper;
        }

        public async Task<CommentResponseDTO> Handle(CreateCommentCommand request, CancellationToken cancellationToken)
        {
            var validator = new CreateCommentCommandValidator();
            var validationResult = validator.Validate(request);

            if (!validationResult.IsValid)
            {
                throw new ValidationException(validationResult.Errors);
            }

            var comment = _mapper.Map<CommentEntity>(request.commentRequestDTO);

            var res = await _commentRepository.CreateAsync(comment);
            return _mapper.Map<CommentResponseDTO>(res);
        }
    }
}
