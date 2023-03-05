using Jamify.BE.Models;
using Microsoft.Data.SqlClient;
using System.Data;

namespace Jamify.BE.Services
{
    public class HashtagWithMappingRepositiory : IHashtagWithMappingRepositiory
    {
        private readonly IDataLayer _dataLayer;

        public HashtagWithMappingRepositiory(IDataLayer dataLayer)
        {
            this._dataLayer = dataLayer;
        }

        public Hashtag CreateHashtagIfNotExist(string name)
        {
            if (_dataLayer.GetDataTable($"SELECT * FROM Hashtag WHERE Name = '{name}'").Rows.Count > 0)
            {
                var query = $"INSERT INTO Hashtag (Name) OUTPUT INSERTED.* VALUES ('{name}')";
                _dataLayer.GetDataTable(query);
                return new Hashtag()
                {
                    Name = name
                };
            }
            return null; // todo
        }

        public HashtagMapping CreateHashtagOnPost(string hashtagName, Guid postId)
        {
            CreateHashtagIfNotExist(hashtagName);
            var hashtagId = GetHashtagIdByName(hashtagName);
            var query = $"INSERT INTO HashtagMapping (HashtagId, PostId) OUTPUT INSERTED.* VALUES ('{hashtagId}', '{postId}')";
            using (var cmd = new SqlCommand(query, _dataLayer.Connection))
            {
                return GetHashtagMapByRow((DataRow)cmd.ExecuteScalar());
            }
            //_hashtagWithMappingRepository.CreateHashtagOnPost(_postRepository.Create(post));
        }

        public void DeleteHashtagMapping(Guid postId, Guid hashtagId)
        {
            var query = $"SELECT + FROM HashtagMapping WHERE PostId = '{postId}' AND HashtagId = '{hashtagId}'";
            if (_dataLayer.GetDataTable(query) != null)
            {
                query = $"DELETE FROM HashtagMapping WHERE PostId = '{postId}' AND HashtagId = '{hashtagId}'";
                var dt = _dataLayer.GetDataTable(query);
            }

        }

        public Guid GetHashtagIdByName(string hashtagName)
        {
            var query = $"SELECT Id FROM Hashtag WHERE Name = '{hashtagName}'";
            var dt = _dataLayer.GetDataTable(query);
            var hashtag = GetHashtagByRow(dt.Rows[0]);
            return hashtag.Id;
        }

        public void DeleteHashtag(Guid hashtagId)
        {
            var query = $"DELETE FROM Hashtag WHERE Id = '{hashtagId}'";
            _dataLayer.GetDataTable(query);
        }

        public Hashtag GetHashtagById(Guid hashtagId)
        {
            var query = $"SELECT * FROM Hashtag WHERE Id = '{hashtagId}'";
            var dt = _dataLayer.GetDataTable(query);
            return GetHashtagByRow(dt.Rows[0]);
        }

        private Hashtag GetHashtagByRow(DataRow dr)
        {
            return new Hashtag()
            {
                Id = dr.Field<Guid>("Id"),
                Name = dr.Field<string>("Name")
            };
        }

        private HashtagMapping GetHashtagMapByRow(DataRow dr)
        {
            return new HashtagMapping()
            {
                Id = dr.Field<Guid>("Id"),
                HashtagId = dr.Field<Guid>("HashtagId"),
                PostId = dr.Field<Guid>("PostId"),
            };
        }
    }
}
