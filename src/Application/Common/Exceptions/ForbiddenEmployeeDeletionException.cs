using System;

namespace CarsManager.Application.Common.Exceptions
{
    public class ForbiddenEmployeeDeletionException : Exception
    {
        public ForbiddenEmployeeDeletionException(string message)
            : base(message)
        {
        }
    }
}
