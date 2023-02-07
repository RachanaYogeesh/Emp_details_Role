using Emp_Details_Role.API.Data;
using Emp_Details_Role.API.Models.Domain;
using Microsoft.EntityFrameworkCore;

namespace Emp_Details_Role.API.Repositories
{
    public class EmpDetRepository : IEmpDetRepository
    {
        private readonly EmpDbContext empDbContext;

        public EmpDetRepository(EmpDbContext empDbContext)
        {
            this.empDbContext = empDbContext;
        }
        
        public async Task<IEnumerable<EmpDet>> GetAllAsync()
        {
            return await empDbContext.EmpDets
                .Include(x=>x.EmpRole)
                .ToListAsync();
        }

        public async Task<EmpDet> GetAsync(int id)
        {
            var emp = empDbContext.EmpDets
                .Include(x => x.EmpRole)
                .FirstOrDefaultAsync(x => x.Id == id);
            return await emp;
        }
        public async Task<EmpDet> AddAsync(EmpDet emp)
        {
            await empDbContext.AddAsync(emp);
            await empDbContext.SaveChangesAsync();
            return emp;
        }

        public async Task<EmpDet> DeleteAsync(int id)
        {
            var existemp = await empDbContext.EmpDets.FindAsync(id);
            if (existemp == null)
            {
                return null;
            }
            empDbContext.EmpDets.Remove(existemp);
            await empDbContext.SaveChangesAsync();

            return existemp;
        }
        public async Task<EmpDet> UpdateAsync(int id, EmpDet emp)
        {
            var existemp = await empDbContext.EmpDets.FindAsync(id);
            if (existemp != null)
            {
                existemp.FName = emp.FName;
                existemp.LName = emp.LName;
                existemp.Email = emp.Email;
                existemp.Contact = emp.Contact;
                existemp.DOJ = emp.DOJ;
                existemp.EmpRoleId = emp.EmpRoleId;
                await empDbContext.SaveChangesAsync();
                return existemp;
            }
            return null;
        }
    }
}
