using Emp_Details_Role.API.Data;
using Emp_Details_Role.API.Models.Domain;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace Emp_Details_Role.API.Repositories
{
    public class EmpRoleRepository : IEmpRoleRepository
    {
        private readonly EmpDbContext empDbContext;
        public EmpRoleRepository(EmpDbContext empDbContext)
        {
            this.empDbContext = empDbContext;
        }
        public async Task<IEnumerable<EmpRole>> GetAllAsync()
        {
            return await empDbContext.EmpRoles.ToListAsync();
        }


        public async Task<EmpRole> GetAsync(int id)
        {
            var emprole = empDbContext.EmpRoles.FirstOrDefaultAsync(x => x.Id == id);
            return await emprole;
        }

        public async Task<EmpRole> AddAsync(EmpRole emprole)
        {
            await empDbContext.AddAsync(emprole);
            await empDbContext.SaveChangesAsync();
            return emprole;
        }

        public async Task<EmpRole> DeleteAsync(int id)
        {
            var emprole = await empDbContext.EmpRoles.FirstOrDefaultAsync(x => x.Id == id);
            if (emprole == null)
            {
                return null;
            }
            empDbContext.EmpRoles.Remove(emprole);
            await empDbContext.SaveChangesAsync();

            return emprole;
        }

        public async Task<EmpRole> UpdateAsync(int id, EmpRole emprole)
        {
            var existrole = await empDbContext.EmpRoles.FirstOrDefaultAsync(x => x.Id == id);
            if (existrole == null)
            {
                return null;
            }
            existrole.RoleName = emprole.RoleName;
            existrole.DeptName = emprole.DeptName;
            await empDbContext.SaveChangesAsync();

            return existrole;
        }
    }
}
