using CarsManager.Application.Common.Interfaces;
using System;

namespace CarsManager.Infrastructure.Services
{
    public class DateTimeService : IDateTime
    {
        public DateTime Now => DateTime.Now;
        public DateTime Today => DateTime.Today;
    }
}
