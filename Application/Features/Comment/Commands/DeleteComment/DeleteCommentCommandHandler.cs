using Application.Contracts;
using AutoMapper;
using FluentValidation;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Comment.Commands.DeleteComment
{
    public class DeleteCommentCommandHandler : IRequestHandler<DeleteCommentCommand, Unit>
    {
        private readonly ICommentRepository _commentRepository;
        private readonly IMapper _mapper;
        public DeleteCommentCommandHandler(ICommentRepository commentRepository, IMapper mapper)
        {
            _commentRepository = commentRepository;
            _mapper = mapper;
        }
        public async Task<Unit> Handle(DeleteCommentCommand request, CancellationToken cancellationToken)
        {
            var validator = new DeleteCommentCommandValidator();
            var validationResult = validator.Validate(request);
            if (!validationResult.IsValid)
            {
                throw new ValidationException(validationResult.Errors);
            }

            var comment = await _commentRepository.GetByIdAsync(request.Id);

            if (comment != null)
            {
                throw new Exception($"Comment with id {request.Id} does't exist!");
            }
            // before this we have to check if the user is trying to delete his comment
            await _commentRepository.DeleteAsync(request.Id);

            return Unit.Value;
        }
    }
}
