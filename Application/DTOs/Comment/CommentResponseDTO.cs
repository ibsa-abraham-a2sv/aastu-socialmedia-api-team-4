using Application.DTOs.Common;
using Application.DTOs.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs.Comment
{
    public class CommentResponseDTO : BaseDto
    {
        public int UserId { get; set; }
        public UserResponseDto? User { get; set; }
        public int PostId { get; set; }
        public string? Text { get; set; }
    }
}
