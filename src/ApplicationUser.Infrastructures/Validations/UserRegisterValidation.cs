using ApplicationUser.Domain.Commands;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationUser.Infrastructures.Validations
{
    public class UserRegisterValidation : AbstractValidator<UserRegisterCommand>
    {
        public UserRegisterValidation()
        {
            RuleFor(s => s.Username)
                .NotNull().WithMessage("Username is required")
                .WithErrorCode("400")
                .NotEmpty().WithMessage("Username is required")
                .WithErrorCode("400");
            RuleFor(s => s.Password)
                .NotNull().WithMessage("Password is required").WithErrorCode("400")
                .NotEmpty().WithMessage("Password is required").WithErrorCode("400");
            RuleFor(s => s.Email)
                .NotNull().WithMessage("Email is required").WithErrorCode("400")
                .NotEmpty().WithMessage("Email is required").WithErrorCode("400");
        }
    }
}
