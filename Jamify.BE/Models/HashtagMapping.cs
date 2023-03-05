namespace Jamify.BE.Models
{
    public class HashtagMapping
    {
        public Guid Id { get; set; }

        public Guid PostId { get; set; }

        public Guid HashtagId { get; set; }
    }
}
