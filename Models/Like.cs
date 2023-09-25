using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class Like
    {
        public Guid Id { get; set; } = default!;
        public DateTime CreatedAt { get; set; } = default!;
        public Guid UserId { get; set; } = default!;
        public User User { get; set; } = default!; 
        public Guid TweetId { get; set; } = default!;
        public Tweet Tweet { get; set; } = default!;
    }
}
