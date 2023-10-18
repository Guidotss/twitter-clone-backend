using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace DataTransfer
{
    public class UserCommentDto
    {
        public Guid Id {  get; set; } = default!;
        public string Name { get; set; } = default!; 
        public string Email { get; set; } = default!;
        public string ImageUrl { get; set; } = default!;
        public ICollection<Comment> Comments { get; set; } = new List<Comment>();
    }
}
