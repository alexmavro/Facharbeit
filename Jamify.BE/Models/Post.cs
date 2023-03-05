namespace Jamify.BE.Models
{
    public class Post
    {
        public Guid Id { get; set; }

        public string Title { get; set; } = "";

        public Byte[] Media { get; set; } = new Byte[0];

        public Guid UserId { get; set; }

        public DateTime CreatedAt { get; set; }

        public string MediaType { get; set; } = "";

        public Guid? MediaId { get; set; }

    }
}
