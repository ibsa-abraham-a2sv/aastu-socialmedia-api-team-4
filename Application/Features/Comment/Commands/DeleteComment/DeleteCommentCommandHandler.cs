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
        public Task<Unit> Handle(DeleteCommentCommand request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
