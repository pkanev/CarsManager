using System;

namespace CarsManager.Application.Common.Exceptions
{
    public class InvalidImageTypeException : Exception
    {
        public InvalidImageTypeException(string message)
            : base(message)
        {
        }
    }
}
