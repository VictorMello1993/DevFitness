using System;

namespace DevFitness.Domain.Exceptions
{
    public class MealIsAlreadyInactiveException : Exception
    {
        public MealIsAlreadyInactiveException() : base("A refeição já foi cancelada.")
        {

        }
    }
}
