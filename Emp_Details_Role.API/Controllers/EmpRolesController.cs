using AutoMapper;
using Emp_Details_Role.API.Models.DTO;
using Emp_Details_Role.API.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace Emp_Details_Role.API.Controllers   
{
    [ApiController]
    [Route("[controller]")]
    //[Authorize(Roles="reader")]
    public class EmpRolesController : Controller
    {

        private readonly IEmpRoleRepository emproleRepository;
        private readonly IMapper mapper;

        public EmpRolesController(IEmpRoleRepository emproleRepository, IMapper mapper)
        {
            this.emproleRepository = emproleRepository;
            this.mapper = mapper;
        }



        [HttpGet]
        [Authorize(Roles = "reader")]
        public async Task<IActionResult> GetAllRolesAsync()
        {
            var emproles = await emproleRepository.GetAllAsync();

            var emprolesDTO = mapper.Map<List<Models.DTO.EmpRole>>(emproles);
            // var rolesDTO = new List<Models.DTO.Role>();
            // roles.ToList().ForEach(roles =>
            //{
            //    var roleDTO = new Models.DTO.Role()
            //    {
            //        Id = roles.Id,
            //        RoleName=roles.RoleName,
            //        DeptName=roles.DeptName,
            //    };
            //    rolesDTO.Add(roleDTO);
            //});

            return Ok(emprolesDTO);
        }

        [HttpGet]
        [Route("{id}")]
        [ActionName("GetRoleAsync")]
        [Authorize(Roles = "reader")]
        public async Task<IActionResult> GetRoleAsync(int id)
        {
            var emprole = await emproleRepository.GetAsync(id);

            if (emprole == null)
            {
                return NotFound();
            }
            var roleDTO = mapper.Map<Models.DTO.EmpRole>(emprole);
            return Ok(roleDTO);
        }

        [HttpPost]
        [Authorize(Roles = "writer")]
        public async Task<IActionResult> AddRoleAsync(Models.DTO.AddEmpRoleRequest addempRoleRequest)
        {

            //RequestDTO to Model
            //Pass details to Repo
            //Convert Data to DTO
            var emprole = new Models.Domain.EmpRole
            { 
                RoleName = addempRoleRequest.RoleName,
                DeptName = addempRoleRequest.DeptName,
            };

            var response = await emproleRepository.AddAsync(emprole);

            var emproleDTO = new Models.DTO.EmpRole
            {
                Id=emprole.Id,
                RoleName = emprole.RoleName,
                DeptName = emprole.DeptName,
            };

            return CreatedAtAction(nameof(GetRoleAsync), new { id = emprole.Id }, emproleDTO);
        }

        [HttpDelete]
        [Route("{id}")]
        [Authorize(Roles = "writer")]
        public async Task<IActionResult> DeleteRoleAsync(int id)
        {
            //Get region from Db
            //If not fount,failure
            //if found, convert to DTO
            //return ok
            var emprole = await emproleRepository.DeleteAsync(id);

            if (emprole == null)
            {
                return NotFound();
            }
            var emproleDTO = new Models.DTO.EmpRole
            {
                RoleName = emprole.RoleName,
                DeptName = emprole.DeptName,
            };
            return Ok(emproleDTO);

        }

        [HttpPut]
        [Route("{id}")]
        [Authorize(Roles = "writer")]
        public async Task<IActionResult> UpdateRoleAsync(int id, Models.DTO.UpdateEmpRoleRequest updateEmpRoleRequest)
        {
            var emprole = new Models.Domain.EmpRole()
            {
                RoleName = updateEmpRoleRequest.RoleName,
                DeptName = updateEmpRoleRequest.DeptName,
            };

            emprole = await emproleRepository.UpdateAsync(id, emprole);

            if (emprole == null)
            {
                return NotFound();
            }

            var emproleDTO = new Models.DTO.EmpRole
            {
                Id = emprole.Id,
                RoleName = emprole.RoleName,
                DeptName = emprole.DeptName,
            };

            return Ok(emproleDTO);
        }
    }
}
