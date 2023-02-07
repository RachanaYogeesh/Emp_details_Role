using FluentValidation;

namespace Emp_Details_Role.API.Validators
{
    public class UpdateEmpRoleValidator:AbstractValidator<Models.DTO.UpdateEmpRoleRequest>
    {
        public UpdateEmpRoleValidator()
        {
            RuleFor(x => x.RoleName).NotEmpty();
            RuleFor(x => x.DeptName).NotEmpty();
        }
    }
}
