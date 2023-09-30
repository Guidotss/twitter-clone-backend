using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class Tweet
    {
        public Guid Id { get; set; }
        public string Content { get; set; } = default!;
        public DateTime CreatedAt { get; set; } = default!;
        public DateTime UpdatedAt { get; set; } = default!;
        public Guid UserId { get; set; } = default!;
        public ICollection<Like> Likes { get; set; } = new List<Like>();
        public ICollection<Retweet> Retweets { get; set; } = new List<Retweet>();
        public ICollection<Comment> Comments { get; set; } = new List<Comment>();
    }
}
