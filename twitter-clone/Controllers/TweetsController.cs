using DataAccess.Repository.IRepository;
using DataTransfer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Routing;
using Models;
using System.Runtime.InteropServices;
using System.Security.Cryptography;
using twitter_clone.Services;

namespace twitter_clone.Controllers
{
    [Route("api/tweets")]
    [ApiController]
    public class TweetsController : ControllerBase
    {
        private readonly CheckUUID checkUUID;
        private readonly IUnitOfWork _unitOfWork;

        public TweetsController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            checkUUID = new CheckUUID();
        }

        [HttpGet]
        public async Task<IActionResult> GetAllTweets()
        {
            try
            {
                var userFromDb = await _unitOfWork.User.GetAllAsync(null, null, "Tweets,Comments,Likes,Retweets");
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


        [HttpGet("{tweet_id}")]

        public async Task<IActionResult> GetTweetsById()
        {
            string tweetId = Request.RouteValues["tweet_id"]?.ToString()!;
            Guid parsedTweetId = checkUUID.IsValid(tweetId);
            if (parsedTweetId == Guid.Empty)
            {
                return BadRequest(new { ok = false, error = "Invalid id" });
            }
            try
            {
                var tweetFromDb = await _unitOfWork.Tweet.GetFirst(t => t.Id == parsedTweetId, "Likes,Comments,Retweets"); 
                if (tweetFromDb == null)
                {
                    return NotFound(new { ok = false, error = "Tweet not found" });
                }
                
                var userFromDb = await _unitOfWork.User.GetAsync(tweetFromDb.UserId);
                if (userFromDb == null)
                {
                    return NotFound(new { ok = false, error = "User not found" });
                }

                var userData = new { id = userFromDb.Id, name = userFromDb.Name, email = userFromDb.Email, imageUrl = userFromDb.ImageUrl };
                var tweet = new { id = tweetFromDb.Id, content = tweetFromDb.Content, gitUrl = tweetFromDb.GifUrl, imageUrl = tweetFromDb.ImageUrl, createdAt = tweetFromDb.CreatedAt, user = userData, retweets = tweetFromDb.Retweets, likes = tweetFromDb.Likes };
                var commentsData = await _unitOfWork.Comments.GetCommentUsers(tweetFromDb.Id);
                

                return Ok(new {ok = true, tweet,commentsData }); 
                
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { ok = false, error = "Internal server error", message = ex.Message });
            }
        }


        [HttpGet]
        [Route("users/{user_id}")]
        public async Task<IActionResult> GetTweetsByUser()
        {
            string userId = Request.RouteValues["user_id"]?.ToString()!;

            Guid parsedUserId = checkUUID.IsValid(userId);
            if (parsedUserId == Guid.Empty)
            {
                return BadRequest(new { ok = false, error = "Invalid id" });
            }
            try
            {
                var userFromDb = await _unitOfWork.User.GetFirst(u => u.Id == parsedUserId, "Tweets");
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
                var userFromDb = await _unitOfWork.User.GetAsync(tweet.UserId);
                if (userFromDb == null)
                {
                    return StatusCode(400, new { ok = false, error = "User not found" });
                }

                var newTweet = new Tweet
                {
                    Content = tweet.Content,
                    UserId = tweet.UserId,
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow,
                    GifUrl = tweet.GifUrl,
                    ImageUrl = tweet.ImageUrl,
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
            Guid parsedTweetId = checkUUID.IsValid(tweetId);
            if (parsedTweetId == Guid.Empty)
            {
                return BadRequest(new { ok = false, error = "Invalid Id" });
            }
            try
            {
                var tweetFromDb = await _unitOfWork.Tweet.GetAsync(parsedTweetId);
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
                    UserId = userFromDb.Id,
                    TweetId = parsedTweetId, 
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

            Guid parsedTweetId = checkUUID.IsValid(tweetId);
            if (parsedTweetId == Guid.Empty)
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
                var tweetFromDb = await _unitOfWork.Tweet.GetAsync(parsedTweetId);
                if (tweetFromDb == null)
                {
                    return NotFound(new { ok = false, error = "Tweet not found" });
                }
                var isLiked = await _unitOfWork.Like.IsLiked(likeData.UserId, parsedTweetId);
                if (isLiked)
                {
                    await _unitOfWork.Like.RemoveLike(likeData.UserId, parsedTweetId);
                    await _unitOfWork.Save();
                    return Ok(new { ok = true, message = "Like removed", isLiked = false });
                }

                var newLike = new Like
                {
                    UserId = likeData.UserId,
                    TweetId = parsedTweetId,
                    CreatedAt = DateTime.UtcNow,
                };

                await _unitOfWork.Like.AddAsync(newLike);
                await _unitOfWork.Save();
                return Ok(new { ok = true, like = newLike, isLiked = true });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { ok = false, erorr = "Internal server error", message = ex.Message });
            }
        }
        [HttpPut]
        [Route("retweets/{tweet_id}")]
        public async Task<IActionResult> CreateRetweet(RetweetDto retweetData) {             
            string tweetId = Request.RouteValues["tweet_id"].ToString();

            Guid parsedTweetId = checkUUID.IsValid(tweetId);
            if (parsedTweetId == Guid.Empty)
            {
                return BadRequest(new { ok = false, error = "Invalid Id" });
            }

            try
            {
                var userFromDb = await _unitOfWork.User.GetAsync(retweetData.UserId);
                if (userFromDb == null)
                {
                    return NotFound(new { ok = false, error = "User not found" });
                }
                var tweetFromDb = await _unitOfWork.Tweet.GetAsync(parsedTweetId);
                if (tweetFromDb == null)
                {
                    return NotFound(new { ok = false, error = "Tweet not found" });
                }

                var isRetweeted = await _unitOfWork.Retweet.GetRetweetByUserAndTweet(retweetData.UserId, parsedTweetId);
                if(isRetweeted != null)
                {
                    await _unitOfWork.Retweet.RemoveRetweet(retweetData.UserId, parsedTweetId);
                    return Ok(new { ok = true, message = "Retweet removed", isRetweeted = false });
                }

                var newRetweet = new Retweet
                {
                    UserId = retweetData.UserId,
                    TweetId = parsedTweetId,
                    CreatedAt = DateTime.UtcNow,
                };

                await _unitOfWork.Retweet.AddAsync(newRetweet);
                await _unitOfWork.Save();
                return Ok(new { ok = true, retweet = newRetweet, isRetweeted = true });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { ok = false, erorr = "Internal server error", message = ex.Message });
            }
        }
    }
}
