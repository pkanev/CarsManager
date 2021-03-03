using System;

namespace CarsManager.Application.Vehicles.Exceptions
{
    public class VehicleAlreadyRegisteredException : Exception
    {
        public VehicleAlreadyRegisteredException(string message)
            : base(message)
        {
        }
    }
}
