using Jamify.BE.Models;

namespace Jamify.BE.Services
{
    public interface IUserRepository
    {
        User Create(User user);
        void Delete(Guid userId, string userPassword);
        User GetUserById(Guid userId);
        User Update(User user);
        IEnumerable<Following> GetUserFollowers(Guid userId);
        IEnumerable<Following> GetUserFollowings(Guid userId);
    }
}