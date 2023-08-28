using Application.Contracts;
using AutoMapper;
using Domain.Entities;
using FluentValidation;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Comment.Commands.UpdateComment
{
    public class UpdateCommentCommandHandler : IRequestHandler<UpdateCommentCommand, Unit>
    {
        private readonly ICommentRepository _commentRepository;
        private readonly IMapper _mapper;
        public UpdateCommentCommandHandler(ICommentRepository commentRepository, IMapper mapper)
        {
            _commentRepository = commentRepository;
            _mapper = mapper;
        }
        public async Task<Unit> Handle(UpdateCommentCommand request, CancellationToken cancellationToken)
        {
            var validator = new UpdateCommentCommandValidator();
            var validationResult = await validator.ValidateAsync(request, cancellationToken);

            if (!validationResult.IsValid)
            {
                throw new ValidationException(validationResult.Errors);
            }
            
            var newComment = _mapper.Map<CommentEntity>(request.UpdateCommentDto);

            var oldComment = await _commentRepository.GetByIdAsync(request.Id);

            if (oldComment == null)
            {
                throw new Exception($"Comment with id {request.Id} does't exist!");
            }

            // we have to check wheather the user id in the comment id is equal to the user currently loggedin

            await _commentRepository.UpdateAsync(request.Id, newComment);
            return Unit.Value;
        }
    }
}
