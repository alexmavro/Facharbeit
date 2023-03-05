using Jamify.BE.Models;
using System.Data;
using System.Runtime.InteropServices;
using Microsoft.Data.SqlClient;

namespace Jamify.BE.Services
{
    public class FollowingRepository : IFollowingRepository
    {
        private readonly IDataLayer _dataLayer;

        public FollowingRepository(IDataLayer dataLayer)
        {
            _dataLayer = dataLayer;
        }

        public Following CreateFollowing(Following following)
        {
            var query = $"SELECT * FROM Following WHERE UserId = '{following.UserId}' AND FollowerId = '{following.FollowerId}' ";
            if (_dataLayer.GetDataTable(query).Rows.Count == 0)
            {
                query = $"INSERT INTO Following (UserId, FollowerId) OUTPUT INSERTED.Id VALUES ('{following.UserId}', '{following.FollowerId}')";
                var returnFollowing = new Following();
                using (var cmd = new SqlCommand(query, _dataLayer.Connection))
                {
                    returnFollowing = GetFollowById((Guid)cmd.ExecuteScalar());
                }

                return returnFollowing;
            }
            return null;
        }

        public void DeleteFollowing(Following following)
        {
            var query = $"SELECT * FROM Following WHERE UserId = '{following.UserId}' AND FollowerId = '{following.FollowerId}' ";
            if (_dataLayer.GetDataTable(query).Rows.Count != 0)
            {
                query = $"DELETE FROM Following WHERE Id ='{following.Id}')";
                GetFollowingByRow(_dataLayer.GetDataTable(query).Rows[0]);
            }
        }

        public Following GetFollowingByRow(DataRow dr)
        {
            return new Following()
            {
                Id = dr.Field<Guid>("Id"),
                UserId = dr.Field<Guid>("UserId"),
                FollowerId = dr.Field<Guid>("FollowerId"),
            };
        }

        public Following GetFollowById(Guid id)
        {
            var query = $"SELECT * FROM Following WHERE Id = '{id}'";
            var dr = _dataLayer.GetDataTable(query).Rows[0];
            return GetFollowingByRow(dr);
        }
    }
}
