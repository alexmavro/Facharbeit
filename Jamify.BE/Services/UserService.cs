using Jamify.BE.Models;
using Jamify.BE.ViewModels;

namespace Jamify.BE.Services
{
    public class UserService : IUserService
    {
        private readonly IPostRepository _postRepository;
        private readonly IUserRepository _userRepository;

        public UserService(IPostRepository postRepository, IUserRepository userRepository)
        {
            this._postRepository = postRepository;
            this._userRepository = userRepository;
        }

        public UserViewModel CreateUser(UserViewModel user)
        {
            var dbUser = new User()
            {
                Email = user.Email,
                UserName = user.UserName,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Birthday = user.Birthday,
                Password   = user.Password
            };

            var returnUser = _userRepository.Create(dbUser);

            return GetUserById(returnUser.Id);
        }

        public UserViewModel GetUserById(Guid userId)
        {
            var dbUser = _userRepository.GetUserById(userId);
            var posts = _postRepository.GetPostsByUserId(userId);
            var postIds = posts.Select(post => post.Id).ToList();
            var follower = _userRepository.GetUserFollowers(userId);
            var followerIds = follower.Select(f => f.Id).ToList();
            var followings = _userRepository.GetUserFollowings(userId);
            var followingsIds = followings.Select(f => f.Id).ToList();

            return new UserViewModel()
            {
                Id = dbUser.Id,
                Email = dbUser.Email,
                UserName = dbUser.UserName,
                FirstName = dbUser.FirstName,
                LastName = dbUser.LastName,
                CreatedAt = dbUser.CreatedAt,
                Birthday = dbUser.Birthday,
                Posts = postIds,
                Followers = followerIds,
                Followings = followingsIds
            };

        }

        public MinimalUserInfoViewModel GetMinimalUserInfoByUserId(Guid id)
        {
            var userVM = this.GetUserById(id);

            return new MinimalUserInfoViewModel()
            {
                Id = userVM.Id,
                UserName = userVM.UserName,
                FirstName = userVM.FirstName,
                LastName = userVM.LastName,
                Posts = userVM.Posts,
                FollowerCount = userVM.Followers.Count(),
            };
        }
    }
}
