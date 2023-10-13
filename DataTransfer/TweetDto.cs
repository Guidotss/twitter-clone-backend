using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace DataTransfer
{
    public class TweetDto
    {
        public string Content { get; set; } = default!;
        public string? GifUrl { get; set; }
        public IFormFile? Image { get; set; } = default; 
        public Guid UserId { get; set; } = default!;
    }
}
