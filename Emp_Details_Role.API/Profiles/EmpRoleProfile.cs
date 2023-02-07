using AutoMapper;

namespace Emp_Details_Role.API.Profiles
{
    public class EmpRoleProfile:Profile
    {
        public EmpRoleProfile()
        {
            CreateMap<Models.Domain.EmpRole, Models.DTO.EmpRole>()
                    .ReverseMap();
        }
    }
}
