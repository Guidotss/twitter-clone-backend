using DataAccess.Data;
using DataAccess.Repository.IRepository;
using DataTransfer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.IdentityModel.Tokens;
using Models;
using System.Data.Entity.Validation;
using System.Linq.Expressions;
using System.Text.Json;
using System.Text.Json.Serialization;

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

        public async Task<IActionResult> GetAllTweets()
        {
            try
            {
                var tweetsFromDb = await _unitOfWork.Tweet.GetAllAsync();
                return Ok(new { ok = true, data = tweetsFromDb });
            }
            catch(Exception ex)
            {
                return StatusCode(500, new { ok = false, error = "Internal server error", message = ex.Message });
            }
        }


        [HttpGet("{userId}")]
        public async Task<IActionResult> GetTweetsByUser()
        {
            string userId = Request.RouteValues["userId"].ToString();
           
            bool isValid = Guid.TryParse(userId, out Guid userIdGuid);
            if(!isValid)
            {
                return BadRequest(new { ok = false, error = "Invalid id" }); 
            }
            try
            {
                var userFromdb = await _unitOfWork.User.GetAsync(userIdGuid);
                if (userFromdb == null)
                {
                    return BadRequest(new { ok = false, error = "User not found" });
                }

                var tweetsFromDb = await _unitOfWork.Tweet.GetAllAsync();


                return Ok(new { ok = true, data = userFromdb.Tweets });
            }
            catch(Exception ex)
            {
                return StatusCode(500, new { ok = false, error = "Internal server error", message = ex.Message });
            }
        }


        [HttpPost]
        public async Task<IActionResult> Post([FromBody] TweetDto tweet)
        {
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
    }
}
