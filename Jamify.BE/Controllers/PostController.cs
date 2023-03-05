using Jamify.BE.Models;
using Jamify.BE.Services;
using Jamify.BE.ViewModels;
using Microsoft.AspNetCore.DataProtection.KeyManagement;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Client;

namespace Jamify.BE.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PostController : ControllerBase
    {
        private readonly IPostRepository _postRepository;
        private readonly IPostService _postService;
        private readonly IReactionRepository _reactionRepository;
        private readonly IUserRepository _userRepository;

        public PostController(IPostRepository postRepository, IPostService postService, IReactionRepository reactionRepository, IUserRepository userRepository)
        {
            this._postRepository = postRepository;
            this._postService = postService;
            this._reactionRepository = reactionRepository;
            _userRepository = userRepository;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Post>> Index()
        {
            try
            {
                return Ok(_postRepository.GetAll());
            }
            catch (Exception e)
            {
                return StatusCode(500);
            }
        }

        [HttpGet("media/{id}")]
        public ActionResult<IEnumerable<Post>> GetPostMediaById(Guid id)
        {
            try
            {
                var post = _postRepository.GetById(id);
                var postmedia = post.Media;
                return Ok(postmedia);
            }
            catch (Exception e)
            {
                return StatusCode(500);
            }
        }

        [HttpPost("react/{postId}")]
        public ActionResult<UserReaction> SetUserReaction([FromBody] UserReaction reaction)
        {
            try
            {
                UserReaction returnReaction;
                var existingUserReaction = _reactionRepository.GetReactionByUserAndPostId(reaction.PostId, reaction.UserId);
                if (existingUserReaction == null)
                {
                    returnReaction = _reactionRepository.CreateReaction(reaction);
                }
                else
                {
                    returnReaction = _reactionRepository.UpdateReaction(reaction);
                }

                return Ok(returnReaction);
            }
            catch (Exception e)
            {
                return StatusCode(500);
            }

        }

        [HttpGet("react/{postId}/{userId}")]
        public ActionResult<UserReaction> GetReactionByUserAndPostId(Guid postId, Guid userId)
        {
            try
            {
                var reaction = _reactionRepository.GetReactionByUserAndPostId(postId, userId);
                return reaction == null ? NotFound() : reaction;
            }
            catch (Exception e)
            {
                return StatusCode(500);
            }
        }

        [HttpDelete("react/{id}")]
        public IActionResult DeleteUserReaction(Guid id)
        {
            try
            {
                _reactionRepository.DeleteReactionById(id);
                return Ok();
            }
            catch (Exception e)
            {
                return StatusCode(500);
            }

        }

        [HttpGet("{id}")]
        public ActionResult<GetPostViewModel> GetPostById(Guid id)
        {
            try
            {
                var dbPost = _postRepository.GetById(id);
                var likes = _reactionRepository.GetLikeCountByPostId(id);
                var dislikes = _reactionRepository.GetDislikeCountByPostId(id);
                var media = File(dbPost.Media, dbPost.MediaType);

                var post = new GetPostViewModel()
                {
                    Id = dbPost.Id,
                    Title = dbPost.Title,
                    Media = media,
                    UserId = dbPost.UserId,
                    LikeCount = likes,
                    DislikeCount = dislikes,
                    CreatedAt = dbPost.CreatedAt
                };
                return post;
            }
            catch (Exception e)
            {
                return StatusCode(500);
            }
        }

        [HttpGet("likes/{postid}")]
        public ActionResult<int> GetLikeCountByPostId(Guid postId)
        {
            try
            {
                var likes = _reactionRepository.GetLikeCountByPostId(postId);
                return likes;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpGet("dislikes/{postid}")]
        public ActionResult<int> GetDislikeCountByPostId(Guid postId)
        {
            try
            {
                var dislikes = _reactionRepository.GetDislikeCountByPostId(postId);
                return dislikes;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpPost("publish")]
        public ActionResult<PostViewModel> PublishPost([FromForm] PostViewModel post)
        {
            try
            {
                var dbPost = _postService.PublishPost(post);
                var likes = _reactionRepository.GetLikeCountByPostId(dbPost.Id);
                var dislikes = _reactionRepository.GetDislikeCountByPostId(dbPost.Id);

                /*            var fileBytes = new Byte[0];
            
                            if (post.Media.Length > 0)
                            {
                                using (var ms = new MemoryStream())
                                {
                                    post.Media.CopyTo(ms);
                                    fileBytes = ms.ToArray();
                                    string s = Convert.ToBase64String(fileBytes);
                                    // act on the Base64 data
                                }
                            }*/

                var returnPost = new PostViewModel()
                {
                    Id = dbPost.Id,
                    Title = dbPost.Title,
                    Media = post.Media,
                    UserId = dbPost.UserId,
                    LikeCount = likes,
                    DislikeCount = dislikes,
                    CreatedAt = dbPost.CreatedAt
                };
                return returnPost;
            }
            catch (Exception e)
            {
                return StatusCode(500);
            }
        }

        [HttpGet("followedByUser/{id}")]
        public ActionResult<IEnumerable<GetPostViewModel>> GetPostFollowedByUser(Guid id)
        {
            try
            {
                var returnedDbPosts = new List<Post>();
                var followings = _userRepository.GetUserFollowings(id);
                foreach (var following in followings)
                {
                    returnedDbPosts.AddRange(_postRepository.GetPostsByUserId(following.UserId));
                }

                var returnPosts = new List<GetPostViewModel>();

                foreach (var dbPost in returnedDbPosts)
                {
                    var likes = _reactionRepository.GetLikeCountByPostId(dbPost.Id);
                    var dislikes = _reactionRepository.GetDislikeCountByPostId(dbPost.Id);
                    var media = File(dbPost.Media, dbPost.MediaType);
                    var post = new GetPostViewModel()
                    {
                        Id = dbPost.Id,
                        Title = dbPost.Title,
                        Media = media,
                        UserId = dbPost.UserId,
                        LikeCount = likes,
                        DislikeCount = dislikes,
                        CreatedAt = dbPost.CreatedAt,
                        MediaType = dbPost.MediaType
                    };
                    returnPosts.Add(post);
                }

                return returnPosts;
            }
            catch (Exception e)
            {
                return StatusCode(500);
            }
        }

        [HttpGet("mediadata/{PostId}")]
        public ActionResult GetMediaById(Guid PostId)
        {
            try
            {
                var post = _postRepository.GetById(PostId);
                var media = File(post.Media, post.MediaType);


                return media;
            }
            catch (Exception e)
            {
                return StatusCode(500);
            }

        }

    }
}
