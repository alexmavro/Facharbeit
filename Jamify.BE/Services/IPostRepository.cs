using Jamify.BE.Models;

namespace Jamify.BE.Services
{
    public interface IPostRepository
    {
        Post Create(Post post);
        void Delete(Guid postId);
        Post GetById(Guid id);
        IEnumerable<Post> GetPostsByUserId(Guid userId);
        Post Update(Post post);
        IEnumerable<Post> GetAll();
    }
}