namespace Emp_Details_Role.API.Models.Domain
{
    public class EmpRole
    {
        public int Id { get; set; }
        public string RoleName { get; set; }

        public string DeptName { get; set; }

        //navigation
        public IEnumerable<EmpDet> EmpDets { get; set; }
    }
}
