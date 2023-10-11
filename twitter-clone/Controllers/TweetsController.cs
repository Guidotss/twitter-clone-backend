using DataAccess.Repository.IRepository;
using DataTransfer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Routing;
using Models;
using System.Runtime.InteropServices;
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

        [HttpGet]
        public async Task<IActionResult> GetAllTweets()
        {
            try
            {
                var userFromDb = await _unitOfWork.User.GetAllAsync(null, null, "Tweets,Comments");
                var tweets = userFromDb.SelectMany(u => u.Tweets).Reverse();

                var userData = userFromDb.Select(u => new { id = u.Id, name = u.Name, email = u.Email, imageUrl = u.ImageUrl }); ;
                var tweetsWithUser = tweets.Select(t => new { tweet = t, user = userData.Where(u => u.id == t.UserId).FirstOrDefault() });


                return Ok(new { ok = true, results = tweetsWithUser });

            }

            catch (Exception ex)
            {
                return StatusCode(500, new { ok = false, error = "Internal server error", message = ex.Message });
            }
        }


        [HttpGet("{user_id}")]
        public async Task<IActionResult> GetTweetsByUser()
        {
            string userId = Request.RouteValues["user_id"].ToString();

            bool isValid = Guid.TryParse(userId, out Guid userIdGuid);
            if (!isValid)
            {
                return BadRequest(new { ok = false, error = "Invalid id" });
            }
            try
            {
                var userFromDb = await _unitOfWork.User.GetFirst(u => u.Id == userIdGuid, "Tweets");
                if (userFromDb == null)
                {
                    return NotFound(new { ok = false, error = "User not found" });
                }

                return Ok(new { ok = true, tweets = userFromDb.Tweets.Reverse() });
            }
            catch (Exception ex)
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
            catch (Exception ex)
            {
                return StatusCode(500, new { ok = false, error = "Internal server error", message = ex.Message });
            }


        }
        [HttpPost]
        [Route("comments/{tweet_id}")]
        public async Task<IActionResult> CreateComment([FromBody] CommentDto commentData)
        {
            if (commentData == null)
            {
                return BadRequest(new { ok = false, error = "Comment is required" });
            }
            string tweetId = Request.RouteValues["tweet_id"].ToString();
            bool isValid = Guid.TryParse(tweetId, out Guid tweetIdGuid);
            if (!isValid)
            {
                return BadRequest(new { ok = false, error = "Invalid Id" });
            }
            try
            {
                var tweetFromDb = await _unitOfWork.Tweet.GetAsync(tweetIdGuid);
                var userFromDb = await _unitOfWork.User.GetAsync(commentData.UserId);

                if (userFromDb == null)
                {
                    return NotFound(new { ok = false, error = "User not found" });
                }
                if (tweetFromDb == null)
                {
                    return NotFound(new { ok = false, error = "Tweet not found" });
                }

                var newComment = new Comment
                {
                    Content = commentData.Content,
                    UserId = commentData.UserId,
                    TweetId = tweetIdGuid,

                };

                await _unitOfWork.Comments.AddAsync(newComment);
                await _unitOfWork.Save();

                return Ok(new { ok = true, comment = newComment });

            }
            catch (Exception ex)
            {
                return StatusCode(500, new { ok = false, error = "Internal server error", message = ex.Message });
            }
        }
        [HttpPut]
        [Route("likes/{tweet_id}")]
        public async Task<IActionResult> UpdateLikes([FromBody] LikeDto likeData)
        {
            string tweetId = Request.RouteValues["tweet_id"].ToString();

            bool isValid = Guid.TryParse(tweetId, out Guid tweetIdGuid);
            if (!isValid)
            {
                return BadRequest(new { ok = false, error = "Invalid Id" });
            }
            try
            {
                var userFromDb = await _unitOfWork.User.GetAsync(likeData.UserId);
                if (userFromDb == null)
                {
                    return NotFound(new { ok = false, error = "User not found" });
                }
                var tweetFromDb = await _unitOfWork.Tweet.GetAsync(tweetIdGuid);
                if (tweetFromDb == null)
                {
                    return NotFound(new { ok = false, error = "Tweet not found" });
                }
                var isLiked = await _unitOfWork.Like.IsLiked(likeData.UserId, tweetIdGuid);
                if (isLiked)
                {
                    await _unitOfWork.Like.RemoveLike(likeData.UserId, tweetIdGuid);
                    await _unitOfWork.Save();
                    return Ok(new { ok = true, message = "Like removed" });
                }

                var newLike = new Like
                {
                    UserId = likeData.UserId,
                    TweetId = tweetIdGuid,
                    CreatedAt = DateTime.UtcNow,
                };

                await _unitOfWork.Like.AddAsync(newLike);
                await _unitOfWork.Save();
                return Ok(new { ok = true, like = newLike });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { ok = false, erorr = "Internal server error", message = ex.Message });
            }
        }
    }
}
