using Emp_Details_Role.API.Models.Domain;

namespace Emp_Details_Role.API.Repositories
{
    public interface ITokenHandler
    {
        Task<string> CreateTokenAsync(User user);
    }
}
