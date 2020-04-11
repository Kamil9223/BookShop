using FluentValidation;
using Ksiegarnia.Contracts.Requests;

namespace Ksiegarnia.Validators
{
    public class PaginationRequestValidator : AbstractValidator<PaginationRequest>
    {
        public PaginationRequestValidator()
        {
            RuleFor(x => x.Page)
                .GreaterThan(0);

            RuleFor(x => x.PageSize)
                .GreaterThan(0);
        }
    }
}
