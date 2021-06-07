using DevFitness.Applictation.Models.InputModels;
using FluentValidation;

namespace DevFitness.Applictation.Validators
{
    public class CreateUserValidator : AbstractValidator<CreateUserInputModel>
    {
        public CreateUserValidator()
        {
            RuleFor(u => u.FullName).NotEmpty().WithMessage("É obrigatório preencher o nome completo.")
                                    .NotNull().WithMessage("É obrigatório preencher o nome completo.")
                                    .Must(ValidateFullName).WithMessage("O nome completo não pode passar de 40 caracteres.");

            RuleFor(u => u.BirthDate).NotEmpty().WithMessage("É obrigatório preencher a data de nascimento.")
                                     .NotNull().WithMessage("É obrigatório preencher a data de nascimento.");            

            RuleFor(u => u).Custom((user, context) =>
            {
                if (user.Height == default(decimal))
                {
                    context.AddFailure("É obrigatório preencher a altura.");
                }
                else if (user.Height < 0)
                {
                    context.AddFailure("A altura não aceita valores negativos.");
                }

                if (user.Weight == default(decimal))
                {
                    context.AddFailure("É obrigatório preencher o peso.");
                }
                else if (user.Weight < 0)
                {
                    context.AddFailure("O peso não aceita valores negativos.");
                }
            });
        }        

        private bool ValidateFullName(string fullName)
        {
            if (!string.IsNullOrWhiteSpace(fullName))
            {
                return fullName.Length <= 40;
            }

            return true;
        }
    }
}
