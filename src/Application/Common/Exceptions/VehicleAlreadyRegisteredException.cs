using System;

namespace CarsManager.Application.Common.Exceptions
{
    public class VehicleAlreadyRegisteredException : Exception
    {
        public VehicleAlreadyRegisteredException(string message)
            : base(message)
        {
        }
    }
}
