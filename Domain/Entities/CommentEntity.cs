﻿
using Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class CommentEntity : BaseDomainEntity
    {
        public int PostId { get; set; }
        public int UserId { get; set; }
        public UserEntity User { get; set; } = null!;
        public PostEntity Post { get; set; } = null!;
        public string? Text { get; set; }
    }
}