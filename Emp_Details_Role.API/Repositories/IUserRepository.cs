using Emp_Details_Role.API.Models.Domain;

namespace Emp_Details_Role.API.Repositories
{
    public interface IUserRepository
    {
        Task<User> AuthenticateUserAsync(string username, string password);
    }
}
