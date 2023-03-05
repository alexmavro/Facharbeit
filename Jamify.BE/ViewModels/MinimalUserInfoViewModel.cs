namespace Jamify.BE.ViewModels
{
    public class MinimalUserInfoViewModel
    { 
        public Guid? Id { get; set; }

        public string UserName { get; set; }

        public string FirstName { get; set; }

        public string? LastName { get; set; }

        public IEnumerable<Guid> Posts { get; set; }

        public int FollowerCount { get; set; }
    }
}
