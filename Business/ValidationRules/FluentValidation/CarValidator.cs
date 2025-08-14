using Entities.Concrede;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.ValidationRules.FluentValidation
{
    public class CarValidator : AbstractValidator<Car>
    {
        public CarValidator()
        {
            RuleFor(c => c.Model).NotEmpty();

            RuleFor(c => c.BrandId).GreaterThan(0);

            RuleFor(c => c.ColorId).GreaterThan(0);

            RuleFor(c => c.Year).NotEmpty();
            RuleFor(c => c.Year).GreaterThan(1500);

            RuleFor(c => c.DailyPrice).NotEmpty();
            RuleFor(c => c.DailyPrice).GreaterThan(0);

            RuleFor(c => c.Plate).NotEmpty();
            RuleFor(c => c.Plate).Matches(@"^\d{2}\s?[A-Z]{1,3}\s?\d{2,4}$");



        }
    }
}
