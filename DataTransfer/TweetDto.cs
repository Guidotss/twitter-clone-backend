using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataTransfer
{
    public class TweetDto
    {
        public string Content { get; set; } = default!;
        public Guid userId { get; set; } = default!;
    }
}
