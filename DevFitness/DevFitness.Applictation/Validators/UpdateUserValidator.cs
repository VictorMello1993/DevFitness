using DevFitness.Applictation.Models.InputModels;
using FluentValidation;

namespace DevFitness.Applictation.Validators
{
    public class UpdateUserValidator : AbstractValidator<UpdateUserInputModel>
    {
        public UpdateUserValidator()
        {
            RuleFor(u => u).Custom((user, context) =>
            {
                if (user.Height == default(decimal))
                {
                    context.AddFailure("É obrigatório preencher a altura");
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
    }
}
