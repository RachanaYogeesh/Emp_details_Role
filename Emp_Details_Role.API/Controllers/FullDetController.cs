using AutoMapper;
using Emp_Details_Role.API.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Emp_Details_Role.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FullDetController : ControllerBase
    {
        private readonly IEmpDetRepository empDetRepository;
        private readonly IMapper mapper;

        public FullDetController(IEmpDetRepository empDetRepository,IMapper mapper)
        {
            this.empDetRepository = empDetRepository;
            this.mapper = mapper;
        }
        [HttpGet]
        [Route("{id}")]
        [ActionName("GetEmployeeAsync")]
        //[Authorize(Roles = "reader")]
        public async Task<IActionResult> GetEmployeeAsync(int id)
        {
            var emp = await empDetRepository.GetAsync(id);

            if (emp == null)
            {
                return NotFound();
            }
            var empDTO = mapper.Map<Models.DTO.EmpDet>(emp);
            return Ok(empDTO);
        }
    }
}
