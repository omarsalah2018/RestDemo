using FluentValidation;
using RestDemo.Dtos;

namespace RestDemo.Validators
{
    public class CreateUserValidator : AbstractValidator<CreateUserDto>
    {
        public CreateUserValidator()
        {
            RuleFor(x => x.Email).EmailAddress().WithMessage("Invalid email format");
            RuleFor(x => x.Name).NotEmpty().WithMessage("Invalid email format").NotNull();
            RuleFor(x => x.Age).GreaterThan(18).WithMessage("يجب ان يكون الشخص بالغ").NotNull();
        }
    }
}