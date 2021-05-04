using System;

namespace CarsManager.Application.Common.Exceptions
{
    public class InvalidAssignVehicleException : Exception
    {
        public InvalidAssignVehicleException(string message)
            : base(message)
        {
        }
    }
}
