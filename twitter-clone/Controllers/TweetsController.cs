using DataAccess.Data;
using DataAccess.Repository.IRepository;
using DataTransfer;
using Microsoft.AspNetCore.Mvc;
using Models;
using System.Data.Entity.Validation;
using System.Text.Json;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace twitter_clone.Controllers
{
    [Route("api/tweet")]
    [ApiController]
    public class TweetsController : ControllerBase
    {
        
        private readonly IUnitOfWork _unitOfWork;

        public TweetsController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        // GET: api/<TweetsController>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<TweetsController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<TweetsController>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] TweetDto tweet)
        {
            var options  = new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            };

           
            try
            {
                var userFromDb = await _unitOfWork.User.GetAsync(tweet.userId);
                if (userFromDb == null)
                {
                    return StatusCode(400, new { ok = false, error = "User not found" });
                }

                var newTweet = new Tweet
                {
                    Content = tweet.Content,
                    UserId = tweet.userId,
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow,
                    Likes = new List<Like>(),
                    Retweets = new List<Retweet>(),
                    Comments = new List<Comment>()
                };

                await _unitOfWork.Tweet.AddAsync(newTweet);
                await _unitOfWork.Save();

                return Ok(new { ok = true, newTweet });

                
            }
            catch(Exception ex)
            {
                return StatusCode(500, new { ok = false, error = "Internal server error", message = ex.Message });
            }            

           
        }

        // PUT api/<TweetsController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<TweetsController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
