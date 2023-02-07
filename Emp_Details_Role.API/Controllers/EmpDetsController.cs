using AutoMapper;
using Emp_Details_Role.API.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace Emp_Details_Role.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class EmpDetsController : Controller
    {
        private readonly IEmpDetRepository empDetRepository;
        private readonly IMapper mapper;
        private readonly IEmpRoleRepository empRoleRepository;

        public EmpDetsController(IEmpDetRepository empDetRepository, IMapper mapper,IEmpRoleRepository empRoleRepository)
        {
            this.empDetRepository = empDetRepository;
            this.mapper = mapper;
            this.empRoleRepository = empRoleRepository;
        }

        [HttpGet]
        [Authorize(Roles = "reader")]
        public async Task<IActionResult> GetAllEmployeesAsync()
        {

            var employees = await empDetRepository.GetAllAsync();

            var employeesDTO = mapper.Map<List<Models.DTO.EmpDet>>(employees);

            return Ok(employeesDTO);
        }

        [HttpGet]
        [Route("{id}")]
        [ActionName("GetEmployeeAsync")]
        [Authorize(Roles = "reader")]
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

        [HttpPost]
        [Authorize(Roles = "writer")]
        public async Task<IActionResult> AddEmployeeAsync(Models.DTO.AddEmpDetRequest addEmpDetRequest)
        {
            if (!await ValidateAddEmployeeRequestAsync(addEmpDetRequest))
            {
                return BadRequest(ModelState);
            }
            //RequestDTO to Model
            //Pass details to Repo
            //Convert Data to DTO
            var emp = new Models.Domain.EmpDet()
            {
                Id = addEmpDetRequest.Id,
                FName = addEmpDetRequest.FName,
                LName = addEmpDetRequest.LName,
                Email = addEmpDetRequest.Email,
                Contact = addEmpDetRequest.Contact,
                DOJ = addEmpDetRequest.DOJ,
                EmpRoleId = addEmpDetRequest.EmpRoleId,
            };

            var response = await empDetRepository.AddAsync(emp);

            var employeeDTO = new Models.DTO.EmpDet
            {
                FName = emp.FName,
                LName = emp.LName,
                Email = emp.Email,
                Contact = emp.Contact,
                DOJ = emp.DOJ,
                EmpRoleId = emp.EmpRoleId,
            };

            return CreatedAtAction(nameof(GetEmployeeAsync), new { id = employeeDTO.Id }, emp);
        }
        [HttpDelete]
        [Route("{id}")]
        [Authorize(Roles = "writer")]
        public async Task<IActionResult> DeleteEmployeeAsync(int id)
        {
            //Get region from Db
            //If not fount,failure
            //if found, convert to DTO
            //return ok
            var emp = await empDetRepository.DeleteAsync(id);

            if (emp == null)
            {
                return NotFound();
            }
            var empDTO = new Models.DTO.EmpDet
            {
                FName = emp.FName,
                LName = emp.LName,
                Email = emp.Email,
                Contact = emp.Contact,
                DOJ = emp.DOJ,
                EmpRoleId = emp.EmpRoleId,
            };
            return Ok(empDTO);

        }
        [HttpPut]
        [Route("{id}")]
        [Authorize(Roles = "writer")]
        public async Task<IActionResult> UpdateEmployeeAsync(int id, Models.DTO.UpdateEmpDetRequest updateEmployeeRequest)
        {
            //if (!await ValidateUpdateEmployeeRequestAsync(updateEmployeeRequest))
            //{
            //    return BadRequest(ModelState);
            //}
            var emp = new Models.Domain.EmpDet()
            {
                FName = updateEmployeeRequest.FName,
                LName = updateEmployeeRequest.LName,
                Email = updateEmployeeRequest.Email,
                Contact = updateEmployeeRequest.Contact,
                DOJ = updateEmployeeRequest.DOJ,
                EmpRoleId = updateEmployeeRequest.EmpRoleId,
            };

            emp = await empDetRepository.UpdateAsync(id, emp);

            if (emp == null)
            {
                return NotFound();
            }

            var empDTO = new Models.DTO.EmpDet
            {
                FName = emp.FName,
                LName = emp.LName,
                Email = emp.Email,
                Contact = emp.Contact,
                DOJ = emp.DOJ,
              EmpRoleId = emp.EmpRoleId,
            };

            return Ok(empDTO);
        }

        private async Task<bool> ValidateAddEmployeeRequestAsync(Models.DTO.AddEmpDetRequest addEmployeeRequest)
        {
            //    if (addEmployeeRequest == null)
            //    {
            //        ModelState.AddModelError(nameof(addEmployeeRequest),
            //            $"Add Employee Data is required");
            //        return false;
            //    }
            //    if (string.IsNullOrWhiteSpace(addEmployeeRequest.FName))
            //    {
            //        ModelState.AddModelError(nameof(addEmployeeRequest.FName), $"{nameof(addEmployeeRequest.FName)} cannot be null or empty space");
            //    }
            //    if (string.IsNullOrWhiteSpace(addEmployeeRequest.LName))
            //    {
            //        ModelState.AddModelError(nameof(addEmployeeRequest.LName), $"{nameof(addEmployeeRequest.LName)} cannot be null or empty space");
            //    }
            //    if (string.IsNullOrWhiteSpace(addEmployeeRequest.Email))
            //    {
            //        ModelState.AddModelError(nameof(addEmployeeRequest.Email), $"{nameof(addEmployeeRequest.Email)} cannot be null or empty space");
            //    }
            //    if (string.IsNullOrWhiteSpace(addEmployeeRequest.Contact))
            //    {
            //        ModelState.AddModelError(nameof(addEmployeeRequest.Contact), $"{nameof(addEmployeeRequest.Contact)} cannot be null or empty space");
            //    }
            //    if (string.IsNullOrWhiteSpace(addEmployeeRequest.DOJ))
            //    {
            //        ModelState.AddModelError(nameof(addEmployeeRequest.DOJ), $"{nameof(addEmployeeRequest.DOJ)} cannot be null or empty space");
            //    }
            var emp = await empRoleRepository.GetAsync(addEmployeeRequest.EmpRoleId);
            if (emp == null)
            {
                ModelState.AddModelError(nameof(addEmployeeRequest.Id), $"{nameof(addEmployeeRequest.EmpRoleId)} is invalid Role");
            }
            if (ModelState.ErrorCount > 0)
            {
                return false;
            }

            return true;
        }
        private async Task<bool> ValidateUpdateEmployeeRequestAsync(Models.DTO.UpdateEmpDetRequest updatedEmployeeRequest)
        {
            //    if (updatedEmployeeRequest == null)
            //    {
            //        ModelState.AddModelError(nameof(updatedEmployeeRequest),
            //            $"Update Employee Data is required");
            //        return false;
            //    }
            //    if (string.IsNullOrWhiteSpace(updatedEmployeeRequest.FName))
            //    {
            //        ModelState.AddModelError(nameof(updatedEmployeeRequest.FName), $"{nameof(updatedEmployeeRequest.FName)} cannot be null or empty space");
            //    }
            //    if (string.IsNullOrWhiteSpace(updatedEmployeeRequest.LName))
            //    {
            //        ModelState.AddModelError(nameof(updatedEmployeeRequest.LName), $"{nameof(updatedEmployeeRequest.LName)} cannot be null or empty space");
            //    }
            //    if (string.IsNullOrWhiteSpace(updatedEmployeeRequest.Email))
            //    {
            //        ModelState.AddModelError(nameof(updatedEmployeeRequest.Email), $"{nameof(updatedEmployeeRequest.Email)} cannot be null or empty space");
            //    }
            //    if (string.IsNullOrWhiteSpace(updatedEmployeeRequest.Contact))
            //    {
            //        ModelState.AddModelError(nameof(updatedEmployeeRequest.Contact), $"{nameof(updatedEmployeeRequest.Contact)} cannot be null or empty space");
            //    }
            //    if (string.IsNullOrWhiteSpace(updatedEmployeeRequest.DOJ))
            //    {
            //        ModelState.AddModelError(nameof(updatedEmployeeRequest.DOJ), $"{nameof(updatedEmployeeRequest.DOJ)} cannot be null or empty space");
            //    }
            var role = await empRoleRepository.GetAsync(updatedEmployeeRequest.EmpRoleId);
            if (role == null)
            {
                ModelState.AddModelError(nameof(updatedEmployeeRequest.EmpRoleId), $"{nameof(updatedEmployeeRequest.EmpRoleId)} is invalid Role");
            }
            if (ModelState.ErrorCount > 0)
            {
                return false;
            }

            return true;
        }
    }
}
