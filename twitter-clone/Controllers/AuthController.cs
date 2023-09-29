using DataAccess.Data;
using DataAccess.Repository.IRepository;
using DataTransfer;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Models;
using System.Security.Policy;
using twitter_clone.Services.Authorization.IAuthorization;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace twitter_clone.Controllers
{
    [Route("api/auth/")]
    [EnableCors("AllowAll")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IAuthorization _authorization;


        public AuthController(IUnitOfWork unitOfWork, IAuthorization authorization)
        {
            _unitOfWork = unitOfWork;
            _authorization = authorization;
        }
        [HttpPost]
        [Route("register")]
        public async Task<IActionResult> RegisterUser([FromBody] RegisterUserDto userData)
        {
            try
            {
                var userFromDb = await _unitOfWork.User.GetUserByEmail(userData.Email);
                if (userFromDb != null)
                {
                    return BadRequest(new { ok = false, error = "Email is already in use" });
                }
                var user = new User
                {
                    Name = userData.Name,
                    Email = userData.Email,
                    Password = _unitOfWork.User.HashPassword(userData.Password)
                };
                await _unitOfWork.User.AddAsync(user);
                await _unitOfWork.Save();
                var token = _authorization.GetToken(user.Email, user.Name);

                return Ok(new { ok = true, newUser = new { name = user.Name, email = user.Email }, token });
            }

            catch (Exception ex)
            {
                return StatusCode(500, new { ok = false, error = "Internal server error", message = ex.Message });
            }
        }
        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> Login([FromBody] LoginDto userData)
        {
            try
            {
                var userFromDb = await _unitOfWork.User.GetUserByEmail(userData.Email);
                if (userFromDb == null)
                {
                    return BadRequest(new { ok = false, error = "Email or password is incorrect" });
                }

                if (!_unitOfWork.User.VerifyPassword(userFromDb, userData.Password))
                {
                    return Unauthorized(new { ok = false, error = "Email or password is incorrect" });
                }

                var token = _authorization.GetToken(userFromDb.Email, userFromDb.Name);

                return Ok(new { ok = true, user = new { name = userFromDb.Name, email = userFromDb.Email }, token });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { ok = false, error = "Internal server error", message = ex.Message });
            }
        }
        [HttpPost]
        [Route("login/google")]
        public IActionResult LoginGoogle([FromBody] RegisterWithPlatformsDto userData)
        {
            try
            {
                var token = _authorization.GetToken(userData.Email, userData.Name);
                return Ok(new { ok = true, user = new { name = userData.Name, email = userData.Email }, token });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { ok = false, error = "Internal server error", message = ex.Message });
            }
        }

        [HttpPost]
        [Route("login/github")]
        public IActionResult LoginGitHub([FromBody] RegisterWithPlatformsDto userData)
        {
            try
            {
                var token = _authorization.GetToken(userData.Email, userData.Name);
                return Ok(new { ok = true, user = new { name = userData.Name, email = userData.Email }, token });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { ok = false, error = "Internal server error", message = ex.Message });
            }
        }
        [HttpGet]
        [Route("renew-token")]
        public async Task<IActionResult> RenewToken([FromHeader(Name = "Authorization")] string authorizationHeader)
        {
            var token = authorizationHeader.Split(" ")[1]; 
            bool isValid = _authorization.VerifyToken(token);
            if (!isValid)
            {
                return Unauthorized(new { ok = false, error = "Invalid token"});
            }
            try
            {
                var email = _authorization.GetUserEmailFromToken(token);
                var userFromDb = await _unitOfWork.User.GetUserByEmail(email);
                if (userFromDb == null)
                {
                    return Unauthorized(new { ok = false, error = "Invalid token" });
                }
                var newToken = _authorization.GetToken(userFromDb.Email, userFromDb.Name);
                return Ok(new { ok = true, user = new { name = userFromDb.Name, email = userFromDb.Email }, token = newToken });
            }
            catch(Exception ex)
            {
                return StatusCode(500, new { ok = false, error = "Internal server error", message = ex.Message }); 
            }
        }
    } 
}
