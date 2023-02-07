namespace Emp_Details_Role.API.Models.DTO
{
    public class AddEmpDetRequest
    {
        public int Id { get; set; }
        public string FName { get; set; }
        public string LName { get; set; }
        public string Email { get; set; }
        public string DOJ { get; set; }
        public string Contact { get; set; }
        public int EmpRoleId { get; set; }
    }
}
