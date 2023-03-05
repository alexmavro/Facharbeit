
namespace Jamify.BE.ViewModels
{
    public class UserViewModel
    {
        public Guid? Id { get; set; }

        public string Email { get; set; }

        public string UserName { get; set; }

        public string FirstName { get; set; }

        public string? LastName { get; set; }

        public DateTime CreatedAt { get; set; }

        public DateTime Birthday { get; set; }

        public IEnumerable<Guid> Posts { get; set; }

        public IEnumerable<Guid> Followings;

        public IEnumerable<Guid> Followers;

        public string Password { get; set; }
    }
}
