using Emp_Details_Role.API.Models.Domain;

namespace Emp_Details_Role.API.Repositories
{
    public interface IEmpDetRepository
    {
        Task<IEnumerable<EmpDet>> GetAllAsync();
        Task<EmpDet> GetAsync(int id);

        Task<EmpDet> AddAsync(EmpDet emp);

        Task<EmpDet> DeleteAsync(int id);

        Task<EmpDet> UpdateAsync(int id, EmpDet emp);
    }
}
