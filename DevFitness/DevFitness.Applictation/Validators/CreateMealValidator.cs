using DevFitness.Applictation.Models.InputModels;
using FluentValidation;

namespace DevFitness.Applictation.Validators
{
    public class CreateMealValidator : AbstractValidator<CreateMealInputModel>
    {
        public CreateMealValidator()
        {
            RuleFor(m => m.Calories).NotEmpty().WithMessage("É obrigatório preencher o valor das calorias.")
                                    .NotNull().WithMessage("É obrigatório preencher o valor das calorias.")
                                    .Must(ValidateCalories).WithMessage("O valor das calorias não pode ser negativo.");

            RuleFor(m => m.Description).NotEmpty().WithMessage("É obrigatório preencher a descrição da refeição.")
                                       .NotNull().WithMessage("É obrigatório preencher a descrição da refeição.")
                                       .Must(ValidateDescription).WithMessage("A descrição não pode passar de 60 caracteres");
        }

        private bool ValidateCalories(int calories)
        {
            if(calories != default(int))
            {
                return calories > 0;
            }

            return true;
        }

        private bool ValidateDescription(string description)
        {
            if (!string.IsNullOrWhiteSpace(description))
            {
                return description.Length <= 60;
            }

            return true;
        }
    }
}
