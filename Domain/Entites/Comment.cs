
using Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entites
{
    public class Comment : BaseDomainEntity
    {
        public int PostId { get; set; }
        public string? Text { get; set; }
    }
}
