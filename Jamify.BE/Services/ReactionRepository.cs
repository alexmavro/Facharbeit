using Jamify.BE.Models;
using Microsoft.Data.SqlClient;
using System.Data;

namespace Jamify.BE.Services
{
    public class ReactionRepository : IReactionRepository
    {
        private readonly IDataLayer _dataLayer;

        public ReactionRepository(IDataLayer dataLayer)
        {
            this._dataLayer = dataLayer;
        }

        public UserReaction CreateReaction(UserReaction reaction)
        {


            var query = $"INSERT INTO UserReaction (UserId, PostId, Reaction) OUTPUT INSERTED.Id VALUES ('{reaction.UserId}', '{reaction.PostId}', '{reaction.Reaction}')";

            using (var cmd = new SqlCommand(query, _dataLayer.Connection))
            {
                var reactionId = (Guid)cmd.ExecuteScalar();
                return GetReactionById(reactionId);
            }


        }

        public UserReaction UpdateReaction(UserReaction reaction)
        {


            var query = $"UPDATE UserReaction SET [Reaction] = '{reaction.Reaction}' WHERE Id = '{reaction.Id}'";

            using (var cmd = new SqlCommand(query, _dataLayer.Connection))
            {
                cmd.ExecuteNonQuery();
                return GetReactionById(reaction.Id.Value);
            }

        }

        public UserReaction GetReactionById(Guid id)
        {
            var query = $"SELECT * FROM UserReaction WHERE Id = '{id}'";
            var dt = _dataLayer.GetDataTable(query);
            var returnReaction = GetReactionByRow(dt.Rows[0]);

            return returnReaction;

        }

        public void DeleteReactionById(Guid id)
        {

            var query = $"DELETE FROM UserReaction WHERE Id ='{id}'";
            using (var cmd = new SqlCommand(query, _dataLayer.Connection))
            {
                cmd.ExecuteNonQuery();
            }

            

        }

        public int GetLikeCountByPostId(Guid postId)
        {
            var query = $"SELECT COUNT(*) FROM UserReaction WHERE PostId = '{postId}' AND [Reaction] ='2'";
            using (var cmd = new SqlCommand(query, _dataLayer.Connection))
            {
                var likes = (int)cmd.ExecuteScalar();
                return likes;
            }
        }

        public int GetDislikeCountByPostId(Guid postId)
        {
            var query = $"SELECT COUNT(*) FROM UserReaction WHERE PostId = '{postId}' AND [Reaction] ='1'";
            using (var cmd = new SqlCommand(query, _dataLayer.Connection))
            {
                var dislikes = (int)cmd.ExecuteScalar();
                return dislikes;
            }
        }

        public UserReaction GetReactionByUserAndPostId(Guid postId, Guid userId)
        {
            var query = $"SELECT * FROM UserReaction WHERE PostId = '{postId}' AND UserId = '{userId}'";
            using (var cmd = new SqlCommand(query, _dataLayer.Connection))
            {
                var dt = _dataLayer.GetDataTable(query);
                if (dt.Rows.Count == 1)
                {
                    return GetReactionByRow(dt.Rows[0]);
                }

                return null;
            }
        }

        private UserReaction GetReactionByRow(DataRow dr)
        {
            return new UserReaction()
            {
                Id = dr.Field<Guid?>("Id"),
                UserId = dr.Field<Guid>("UserId"),
                PostId = dr.Field<Guid>("PostId"),
                Reaction = dr.Field<int>("Reaction"),
            };
        }
    }
}
