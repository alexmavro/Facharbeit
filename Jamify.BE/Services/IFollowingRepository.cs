using System.Data;
using Jamify.BE.Models;

namespace Jamify.BE.Services;

public interface IFollowingRepository
{
    Following CreateFollowing(Following following);
    void DeleteFollowing(Following following);
    Following GetFollowingByRow(DataRow dr);
}