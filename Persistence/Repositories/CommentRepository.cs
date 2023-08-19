using Application.Contracts;
using Application.Contracts.Common;
using Application.DTOs.Comment;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Repositories
{
    public class CommentRepository : GenericRepository<CommentEntity>, ICommentRepository
    {
        private readonly AppDBContext appDBContext;

        public CommentRepository(AppDBContext appDBContext) : base(appDBContext) 
        {
            this.appDBContext = appDBContext;
        }

        public Task<List<CommentEntity>> GetCommentByPostId(int postId)
        {
            var comments = appDBContext.Comments.Where(c => c.PostId == postId);
            return (Task<List<CommentEntity>>)comments;
        }
    }
}
