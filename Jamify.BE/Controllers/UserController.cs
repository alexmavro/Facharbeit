using Jamify.BE.Models;
using Jamify.BE.Services;
using Jamify.BE.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace Jamify.BE.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : Controller
    {
        private readonly IUserRepository _userRepository;
        private readonly IUserService _userService;
        private readonly IFollowingRepository _followingRepository;

        public UserController(IUserRepository userRepository, IUserService userService, IFollowingRepository followingRepository)
        {
            _userRepository = userRepository;
            _userService = userService;
            _followingRepository = followingRepository;
        }
        [HttpGet("{id}")]
        public ActionResult<UserViewModel> GetUserById(Guid id)
        {
            var user = _userService.GetUserById(id);
            return Ok(user);
        }

        [HttpPost("create")]
        public ActionResult<UserViewModel> CreateUser([FromBody] UserViewModel user)
        {
            try
            {
                var returnUser = _userService.CreateUser(user);
                return Ok(returnUser);
            }
            catch (Exception e)
            {
                return StatusCode(500);
            }
        }

        [HttpGet("MinimalInfo/{id}")]
        public ActionResult<MinimalUserInfoViewModel> GetMinimalUserInfoByUserId(Guid id)
        {
            try
            {
                var user = _userService.GetMinimalUserInfoByUserId(id);
                return Ok(user);
            }
            catch (Exception e)
            {
                return StatusCode(500);
            }
            
        }

        [HttpGet("Followers/{id}")]
        public ActionResult<Following> GetFollowersByUserId(Guid id)
        {
            try
            {
                var followers = _userRepository.GetUserFollowers(id);
                return Ok(followers);
            }
            catch (Exception e)
            {
                return StatusCode(500);
            }

        }

        [HttpGet("Followings/{id}")]
        public ActionResult<Following> GetFollowingsByUserId(Guid id)
        {
            try
            {
                var followings = _userRepository.GetUserFollowings(id);
                return Ok(followings);
            }
            catch (Exception e)
            {
                return StatusCode(500);
            }

        }

        [HttpPost("Follow")]
        public ActionResult<Following> FollowUser(Following following)
        {
            try
            {
                var returnFollowing = _followingRepository.CreateFollowing(following);
                return Ok(returnFollowing);
            }
            catch (Exception e)
            {
                return StatusCode(500);
            }

        }
    }
}
