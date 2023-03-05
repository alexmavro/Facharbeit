using Jamify.BE.Models;

namespace Jamify.BE.Services
{
    public interface IHashtagWithMappingRepositiory
    {
        Hashtag CreateHashtagIfNotExist(string name);
        HashtagMapping CreateHashtagOnPost(string hashtagName, Guid postId);
        void DeleteHashtag(Guid hashtagId);
        void DeleteHashtagMapping(Guid postId, Guid hashtagId);
        Hashtag GetHashtagById(Guid hashtagId);
        Guid GetHashtagIdByName(string hashtagName);
    }
}