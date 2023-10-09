using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataTransfer
{
    public class CommentDto
    {
        public Guid UserId { get; set; }
        public string Content { get; set; } = string.Empty;
    }
}
