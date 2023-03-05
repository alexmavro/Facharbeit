using Jamify.BE.Models;
using Jamify.BE.ViewModels;

namespace Jamify.BE.Services
{
    public interface IPostService
    {
        Post PublishPost(PostViewModel post);
    }
}