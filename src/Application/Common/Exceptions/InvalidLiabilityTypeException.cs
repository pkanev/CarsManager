using System;

namespace CarsManager.Application.Common.Exceptions
{
    public class InvalidLiabilityTypeException : Exception
    {
        public InvalidLiabilityTypeException(string message)
            : base(message)
        {
        }
    }
}
