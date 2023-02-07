using FluentValidation;

namespace Emp_Details_Role.API.Validators
{
    public class AddEmpRoleRequestValidator: AbstractValidator<Models.DTO.AddEmpRoleRequest>
    {
        public AddEmpRoleRequestValidator()
        {
            RuleFor(x => x.RoleName).NotEmpty();
            RuleFor(x => x.DeptName).NotEmpty();
        }
    }
}
