using DataAccess.Repository.IRepository;
using DataTransfer;
using Microsoft.AspNetCore.Mvc;
using Models;
using System.Security.Cryptography;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace twitter_clone.Controllers
{
    [Route("api/tweets")]
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
                var userFromDb = await _unitOfWork.User.GetAllAsync(null,null, "Tweets");
                var tweets = userFromDb.SelectMany(u => u.Tweets).Reverse();

                var userData = userFromDb.Select(u => new {  id = u.Id, name = u.Name, email = u.Email, imageUrl = u.ImageUrl } ); ;
                var tweetsWithUser = tweets.Select(t => new { tweet = t, user = userData.Where(u => u.id == t.UserId).FirstOrDefault() }); 
               
               
                return Ok(new { ok = true, results = tweetsWithUser  });

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
                var userFromDb = await _unitOfWork.User.GetFirst(u => u.Id == userIdGuid, "Tweets");

                return Ok(new { ok = true, tweets = userFromDb.Tweets.Reverse() });
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

                var results = new
                {
                    user = new { id = userFromDb.Id, name = userFromDb.Name, email = userFromDb.Email, imageUrl = userFromDb.ImageUrl },
                    tweet = newTweet
                };

                return Ok(new { ok = true, results });
            }
            catch(Exception ex)
            {
                return StatusCode(500, new { ok = false, error = "Internal server error", message = ex.Message });
            }            

           
        }
    }
}
