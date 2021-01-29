using System;

namespace CarsManager.Application.Models.Exceptions
{
    public class NonMatchingMakeException : Exception
    {
        public NonMatchingMakeException(string message)
            : base(message)
        {
        }
    }
}
