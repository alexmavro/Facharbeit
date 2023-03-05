namespace Jamify.BE.Models
{
    public class Following
    {
        public Guid Id { get; set; }

        public Guid UserId { get; set; }

        public Guid FollowerId { get; set; }
    }
}
