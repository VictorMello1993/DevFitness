using System;

namespace DevFitness.Domain.Exceptions
{
    public class UserIsAlreadyInactiveException : Exception
    {
        public UserIsAlreadyInactiveException() : base("Usuário está inativo.")
        {

        }
    }
}
