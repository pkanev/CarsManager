using System;

namespace CarsManager.Application.Common.Exceptions
{
    public class InvalidDeleteOperationException : Exception
    {
        public InvalidDeleteOperationException(string message)
            : base(message)
        {
        }
    }
}
