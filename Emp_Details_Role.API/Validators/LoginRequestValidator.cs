using FluentValidation;

namespace Emp_Details_Role.API.Validators
{
    public class LoginrequestValidator: AbstractValidator<Models.DTO.LoginRequest>
    {
        public LoginrequestValidator()
        {
            RuleFor(x => x.Username).NotEmpty();
            RuleFor(x => x.Password).NotEmpty();
        }
    }
}
