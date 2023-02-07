using AutoMapper;

namespace Emp_Details_Role.API.Profiles
{
    public class EmpDetProfile:Profile
    {
        public EmpDetProfile()
        {
            CreateMap<Models.Domain.EmpDet, Models.DTO.EmpDet>()
                    .ReverseMap();
        }
    }
}
