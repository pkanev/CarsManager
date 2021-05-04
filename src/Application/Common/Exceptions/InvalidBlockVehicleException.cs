using System;

namespace CarsManager.Application.Common.Exceptions
{
    public class InvalidBlockVehicleException : Exception
    {
        public InvalidBlockVehicleException(string message)
            : base(message)
        {
        }
    }
}
