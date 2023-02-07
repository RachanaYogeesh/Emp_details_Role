namespace Emp_Details_Role.API.Models.Domain
{
    public class Role
    {
        public  int Id { get; set; }
        public string Name { get; set; }

        //Navigation
        public List<User_Role> UserRoles { get; set; }
    }
}
