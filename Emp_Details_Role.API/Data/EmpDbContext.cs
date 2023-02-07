using Emp_Details_Role.API.Models.Domain;
using Microsoft.AspNetCore.Rewrite;
using Microsoft.EntityFrameworkCore;
using System.Data;
using System.Runtime.CompilerServices;

namespace Emp_Details_Role.API.Data
{
    public class EmpDbContext:DbContext
    {
        public EmpDbContext(DbContextOptions<EmpDbContext> options) : base(options)
        {

        }
        //protected override void OnModelCreating(ModelBuilder modelBuilder)
        //{
        //    modelBuilder.Entity<User_Role>()
        //        .HasOne(x => x.Role)
        //        .WithMany(y => y.UserRoles)
        //        .HasForeignKey(x => x.RoleId);

        //    modelBuilder.Entity<User_Role>()
        //        .HasOne(x => x.User)
        //        .WithMany(y => y.UserRoles)
        //        .HasForeignKey(x => x.UserId);
        //}
        public DbSet<EmpDet> EmpDets { get; set; }
        public DbSet<EmpRole> EmpRoles { get; set; }

        //public DbSet<User> Users { get; set; }
        //public DbSet<Role> Roles { get; set; }
        //public DbSet<User_Role> Users_Roles { get; set; }

    }
}
