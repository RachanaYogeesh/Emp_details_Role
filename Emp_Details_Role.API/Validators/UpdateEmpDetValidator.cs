using FluentValidation;

namespace Emp_Details_Role.API.Validators
{
    public class UpdateEmpDetValidator:AbstractValidator<Models.DTO.UpdateEmpDetRequest>
    {
        public UpdateEmpDetValidator()
        {
            RuleFor(x => x.FName).NotEmpty();
            RuleFor(x => x.LName).NotEmpty();
            RuleFor(x => x.Email).NotEmpty();
            RuleFor(x => x.DOJ).NotEmpty();
            RuleFor(x => x.Contact).NotEmpty();
        }
    }
}
