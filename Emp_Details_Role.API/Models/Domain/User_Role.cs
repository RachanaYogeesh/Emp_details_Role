namespace Emp_Details_Role.API.Models.Domain
{
    public class User_Role
    {
        public int Id { get; set; }
        //Navigation
        public int UserId { get; set; }
        public User User { get; set; }

        public int RoleId { get; set; }
        public Role Role { get; set; }
    }
}
