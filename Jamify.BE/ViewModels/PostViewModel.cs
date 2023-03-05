using Jamify.BE.Models;
using Microsoft.AspNetCore.Mvc;

namespace Jamify.BE.ViewModels
{
    public class PostViewModel
    {
        public Guid Id { get; set; }

        public string Title { get; set; } = "";

        public IFormFile? Media { get; set; }

        public Guid UserId { get; set; }

        public int LikeCount { get; set; }

        public int DislikeCount { get; set; }

        public DateTime CreatedAt { get; set; }

        public string MediaType { get; set; }
    }
}
