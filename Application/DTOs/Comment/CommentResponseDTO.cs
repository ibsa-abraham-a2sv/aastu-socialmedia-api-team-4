using Application.DTOs.Common;
using Application.DTOs.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.DTOs.Post;
using Domain.Entities;

namespace Application.DTOs.Comment
{
    public class CommentResponseDTO : BaseDto
    {
        public int UserId { get; set; }
        public UserResponseDto User { get; set; } = null!;
        public PostResponseDto Post { get; set; } = null!;
        public int PostId { get; set; }
        public string? Text { get; set; }
    }
}
