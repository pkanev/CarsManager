using System;

namespace CarsManager.Application.Common.Exceptions
{
    public class ForbiddenVehicleDeletionException : Exception
    {
        public ForbiddenVehicleDeletionException(string message)
            : base(message)
        {
        }
    }
}
