using Entities.Concrede;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.ValidationRules.FluentValidation
{
    public class CustomerValidator : AbstractValidator<Customer>
    {
        public CustomerValidator()
        {
            RuleFor(c => c.FirstName).NotEmpty();
            RuleFor(c => c.FirstName).MinimumLength(2);

            RuleFor(c => c.LastName).NotEmpty();
            RuleFor(c => c.LastName).MinimumLength(2);

            RuleFor(c => c.Email).NotEmpty();

            RuleFor(c => c.Phone).NotEmpty();

            RuleFor(c => c.Country).NotEmpty();
            RuleFor(c => c.Country).MinimumLength(2);



        }
    }
}
