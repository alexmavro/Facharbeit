using Jamify.BE.Models;

namespace Jamify.BE.Services
{
    public interface IReactionRepository
    {
        UserReaction CreateReaction(UserReaction reaction);
        void DeleteReactionById(Guid id);
        int GetLikeCountByPostId(Guid postId);
        int GetDislikeCountByPostId(Guid postId);
        UserReaction? GetReactionByUserAndPostId(Guid postId, Guid userId);
        UserReaction UpdateReaction(UserReaction reaction);
    }
}