namespace Jamify.BE.Models
{
    public class UserReaction
    {
        public Guid? Id { get; set; }

        public Guid UserId{ get; set; }

        public Guid PostId { get; set; }

        public int Reaction { get; set; }
    }
}
