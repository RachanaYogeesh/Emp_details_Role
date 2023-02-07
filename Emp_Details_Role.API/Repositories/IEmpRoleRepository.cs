using System.Data;
using Emp_Details_Role.API.Models.Domain;

namespace Emp_Details_Role.API.Repositories
{
    public interface IEmpRoleRepository
    {
        Task<IEnumerable<EmpRole>> GetAllAsync();

        Task<EmpRole> GetAsync(int id);
        Task<EmpRole> AddAsync(EmpRole emprole);

        Task<EmpRole> DeleteAsync(int id);

        Task<EmpRole> UpdateAsync(int id, EmpRole emprole);
    }
}
