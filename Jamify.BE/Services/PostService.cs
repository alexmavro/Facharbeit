using System.Net;
using Jamify.BE.Models;
using Jamify.BE.ViewModels;
using Microsoft.AspNetCore.DataProtection.KeyManagement;
using Microsoft.AspNetCore.Mvc;

namespace Jamify.BE.Services
{
    public class PostService : IPostService
    {
        private readonly IPostRepository _postRepository;
        private readonly IHashtagWithMappingRepositiory _hashtagWithMappingRepositiory;
        private readonly IReactionRepository _reactionRepository;

        public PostService(IPostRepository postRepository, IHashtagWithMappingRepositiory hashtagWithMappingRepositiory, IReactionRepository reactionRepository)
        {
            this._postRepository = postRepository;
            this._hashtagWithMappingRepositiory = hashtagWithMappingRepositiory;
            _reactionRepository = reactionRepository;
        }

        public Post PublishPost(PostViewModel post)
        {
            var media = new Byte[0];

            if (post.Media.Length > 0)
            {
                using (var ms = new MemoryStream())
                {
                    post.Media.CopyTo(ms);
                    media = ms.ToArray();
                    //string s = Convert.ToBase64String(media);
                    // act on the Base64 data
                }
            }

            var dbPost = new Post()
            {
                Title = post.Title,
                Media = media,
                UserId = post.UserId,
                MediaType = post.MediaType,
            };

            return _postRepository.Create(dbPost);

            
        }
    }
}
