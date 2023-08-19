using Application.Contracts.Common;
using Application.DTOs.Comment;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Contracts
{
    public interface ICommentRepository : IGenericRepository<CommentEntity>
    {
        Task<List<CommentEntity>> GetCommentByPostId(int postId);
    }
}
