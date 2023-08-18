using Application.Contracts.Common;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Contracts.Comment
{
    public interface ICommentRepository : IGenericRepository<CommentEntity>
    {
    }
}
