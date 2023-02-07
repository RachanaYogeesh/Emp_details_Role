using FluentValidation;

namespace Emp_Details_Role.API.Validators
{
    public class AddEmpDetValidator:AbstractValidator<Models.DTO.AddEmpDetRequest>
    {
        public AddEmpDetValidator()
        {
            RuleFor(x => x.FName).NotEmpty();
            RuleFor(x => x.LName).NotEmpty();
            RuleFor(x => x.Email).NotEmpty();
            RuleFor(x => x.DOJ).NotEmpty();
            RuleFor(x => x.Contact).NotEmpty();
            RuleFor(x=>x.EmpRoleId).NotEmpty();
        }
    }
}
