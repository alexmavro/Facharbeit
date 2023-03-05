using Jamify.BE.ViewModels;

namespace Jamify.BE.Services;

public interface IUserService
{
    UserViewModel GetUserById(Guid userId);
    UserViewModel CreateUser(UserViewModel user);
    MinimalUserInfoViewModel GetMinimalUserInfoByUserId(Guid id);
}