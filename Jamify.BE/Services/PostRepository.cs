using Jamify.BE.Models;
using Microsoft.Data.SqlClient;
using System.Data;
using System.Net.WebSockets;

namespace Jamify.BE.Services
{
    public class PostRepository : IPostRepository
    {
        private readonly IDataLayer _dataLayer;

        public PostRepository(IDataLayer dataLayer)
        {
            _dataLayer = dataLayer;
        }

        public Post Create(Post post)
        {
            var mediaQuery = $"INSERT INTO MediaData (MediaBytes, MediaType) OUTPUT INSERTED.Id VALUES (@byte_param, '{post.MediaType}')";
            var media = new Guid();
            var byteParam = new SqlParameter("@byte_param", SqlDbType.VarBinary)
            {
                Direction = ParameterDirection.Input,
                Size = post.Media.Length,
                Value = post.Media
            };
            using (var cmd = new SqlCommand(mediaQuery, _dataLayer.Connection))
            {
                cmd.Parameters.Add(byteParam);
                media = (Guid)cmd.ExecuteScalar();
            }
            var query = $"INSERT INTO Post (Title, MediaId, UserId) OUTPUT INSERTED.* VALUES ('{post.Title}', '{media}', '{post.UserId}')";
            
            
            using (var cmd = new SqlCommand(query, _dataLayer.Connection))
            {
                
                return GetById((Guid)cmd.ExecuteScalar());
            }
        }

        public IEnumerable<Post> GetPostsByUserId(Guid userId)
        {
            var postList = new List<Post>();
            var query = $"SELECT * From Post WHERE UserId = '{userId}'";
            var dt = _dataLayer.GetDataTable(query);
            foreach (DataRow dr in dt.Rows)
            {
                postList.Add(GetPostByRow(dr));
            }
            return postList;
        }

        public Post GetById(Guid id)
        {
            var query = $"SELECT * FROM Post WHERE Id = '{id}'";
            var dt = _dataLayer.GetDataTable(query);
            if(dt.Rows.Count == 0)
            {
                return null;
            }

            return GetPostByRow(dt.Rows[0]);
            
        }

        public Post Update(Post post)
        {
            if(GetById(post.Id) != null)
            {
                var query = $"UPDATE Post SET Title = '{post.Title}' WHERE Id = '{post.Id}'";
                var dt = _dataLayer.GetDataTable(query);
            }
            return post;
        }

        public void Delete(Guid postId)
        {
            var query = $"DELETE FROM Post WHERE Id = '{postId}'";
            using (var cmd = new SqlCommand(query, _dataLayer.Connection))
            {
                cmd.ExecuteNonQuery();
            }
            query = $"DELETE FROM HashtagMapping WHERE PostId = '{postId}'";
            using (var cmd = new SqlCommand(query, _dataLayer.Connection))
            {
                cmd.ExecuteNonQuery();
            }
            query = $"DELETE FROM UserReaction WHERE PostId = '{postId}'";
            using (var cmd = new SqlCommand(query, _dataLayer.Connection))
            {
                cmd.ExecuteNonQuery();
            }

        }

        private Post GetPostByRow(DataRow dr)
        {
            var post = new Post()
            {
                Id = dr.Field<Guid>("Id"),
                Title = dr.Field<string>("Title"),
                UserId = dr.Field<Guid>("UserId"),
                CreatedAt = dr.Field<DateTime>("CreatedAt"),
                MediaId = dr.Field<Guid>("MediaId")
            };

            var query = $"SELECT * FROM [MediaData] WHERE Id = '{post.MediaId}'";
            var mediaDr = _dataLayer.GetDataTable(query);
            var media = mediaDr.Rows[0].Field<Byte[]>("MediaBytes");
            var mediaType = mediaDr.Rows[0].Field<string>("MediaType");

            post.Media = media;
            post.MediaType = mediaType;

            return post;
        }

        public IEnumerable<Guid> GetPostIdsWithCertainHashtagId(Guid hashtagId)
        {
            var postIdList = new List<Guid>();
            var query = $"SELECT PostId FROM HashtagMapping WHERE HashtagId = '{hashtagId}'";
            DataTable dt = _dataLayer.GetDataTable(query);

            foreach (DataRow dr in dt.Rows)
            {
                postIdList.Add(new Guid(dr[0].ToString()));
            }
            return postIdList;
        }

        public IEnumerable<Post> GetAll()
        {
            var postList = new List<Post>();
            var query = "SELECT * FROM Post ";
            var dt = _dataLayer.GetDataTable(query);
            foreach (DataRow dr in dt.Rows)
            {
                postList.Add(GetPostByRow(dr));
            }
            return postList;
        }
    }
}
