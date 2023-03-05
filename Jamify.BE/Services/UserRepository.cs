using Jamify.BE.Models;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Hosting;
using System.Data;
using System.Data.Common;

namespace Jamify.BE.Services
{
    public class UserRepository : IUserRepository
    {
        private readonly IDataLayer _dataLayer;

        public UserRepository(IDataLayer dataLayer)
        {
            _dataLayer = dataLayer;
        }

        public User Create(User user)
        {
            var birthday = user.Birthday.ToString("yyyy-MM-dd");
            var query = $"INSERT INTO [User] (Email, UserName, FirstName, LastName, Birthday, Password) OUTPUT INSERTED.Id VALUES ('{user.Email}', '{user.UserName}', '{user.FirstName}', '{user.LastName}', '{birthday}', '{user.Password}')";
            using (var cmd = new SqlCommand(query, _dataLayer.Connection))
            {
                return GetUserById((Guid)cmd.ExecuteScalar());
            }
            
        }

        public User GetUserById(Guid userId)
        {
            /*var query = $"SELECT * FROM User WHERE Id = '{userId}'";*/ //
            var query = $"SELECT * FROM [User] WHERE Id = '{userId}'"; 
            var dt = _dataLayer.GetDataTable(query);

            if (dt.Rows.Count == 0)
            {
                return null;
            }
            return GetUserByRow(dt.Rows[0]);
        }

        public User Update(User user)
        {
            if (GetUserById(user.Id) != null)
            {
                var query = $"UPDATE User SET UserName = '{user.UserName}', Password = '{user.Password}' WHERE Id = '{user.Id}'";
                var dt = _dataLayer.GetDataTable(query);
            }
            return user;
        }

        public IEnumerable<Following> GetUserFollowers(Guid userId)
        {
            var query = $"SELECT * FROM Following WHERE UserId = '{userId}'";
            var followersDt = _dataLayer.GetDataTable(query);
            List<Following> followers = new List<Following>();
            foreach (DataRow row in followersDt.Rows)
            {
                followers.Add(
                    new Following()
                    {
                        Id = row.Field<Guid>("Id"),
                        UserId = row.Field<Guid>("UserId"),
                        FollowerId = row.Field<Guid>("FollowerId")
                    });
            }

            return followers;
        }

        public IEnumerable<Following> GetUserFollowings(Guid userId)
        {
            var query = $"SELECT * FROM Following WHERE FollowerId = '{userId}'";
            var followingDt = _dataLayer.GetDataTable(query);
            List<Following> followings = new List<Following>();
            foreach (DataRow row in followingDt.Rows)
            {
                followings.Add(
                    new Following()
                    {
                        Id = row.Field<Guid>("Id"),
                        UserId = row.Field<Guid>("UserId"),
                        FollowerId = row.Field<Guid>("FollowerId")
                    });
            }

            return followings;
        }

        public void Delete(Guid userId, string userPassword)
        {
            if(userPassword == GetUserById(userId).Password)
            {
                var query = $"DELETE FROM User WHERE Id = '{userId}'";
                using (var cmd = new SqlCommand(query, _dataLayer.Connection))
                {
                    cmd.ExecuteNonQuery();
                }
                query = $"DELETE FROM Following WHERE UserId = '{userId}'";
                using (var cmd = new SqlCommand(query, _dataLayer.Connection))
                {
                    cmd.ExecuteNonQuery();
                }
                query = $"DELETE FROM Following WHERE FollowerId = '{userId}'";
                using (var cmd = new SqlCommand(query, _dataLayer.Connection))
                {
                    cmd.ExecuteNonQuery();
                }
                query = $"DELETE FROM Post WHERE UserId = '{userId}'";
                using (var cmd = new SqlCommand(query, _dataLayer.Connection))
                {
                    cmd.ExecuteNonQuery();
                }
                query = $"DELETE FROM UserReaction WHERE UserId = '{userId}'";
                using (var cmd = new SqlCommand(query, _dataLayer.Connection))
                {
                    cmd.ExecuteNonQuery();
                }
            }

            
        }

        private User GetUserByRow(DataRow dr)
        {
            return new User()
            {
                Id = dr.Field<Guid>("Id"),
                Email = dr.Field<string>("Email"),
                UserName = dr.Field<string>("UserName"),
                FirstName = dr.Field<string>("FirstName"),
                LastName = dr.Field<string>("LastName"),
                CreatedAt = dr.Field<DateTime>("CreatedAt"),
                Birthday = dr.Field<DateTime>("Birthday"),
                Password = dr.Field<string>("Password"),
            };
        }
    }
}
