using System;

namespace CarsManager.Application.Common.Exceptions
{
    public class InvalidRoadEntryDataException : Exception
    {
        public InvalidRoadEntryDataException(string message)
            : base(message)
        {
        }
    }
}
