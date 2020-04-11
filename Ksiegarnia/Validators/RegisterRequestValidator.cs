using FluentValidation;
using Ksiegarnia.Contracts.Requests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ksiegarnia.Validators
{
    public class RegisterRequestValidator : AbstractValidator<RegisterRequest>
    {
        public RegisterRequestValidator()
        {
            RuleFor(x => x.Email)
                .NotNull()
                .NotEmpty()
                .EmailAddress();

            RuleFor(x => x.Login)
                .NotNull()
                .NotEmpty()
                .Length(3, 20)
                .Matches("^[a-zA-z ]*$");

            RuleFor(x => x.Password)
                .Length(5, 20);
        }
    }
}
