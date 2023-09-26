using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class Follow
    {
        public Guid Id { get; set; } = default!;
        public DateTime CreatedAt { get; set; } = default!;
        public Guid FollowerId { get; set; } = default!;
        public User Follower { get; set; } = default!;
        public Guid FolloweeId { get; set; } = default!;
        public User Followee { get; set; } = default!; 
    }
}
