using System;

namespace Client.Core.Models.Liabilities
{
    public class LiabilityModel
    {
        public int Id { get; set; }
        public DateTime StartDate { get; set; } = DateTime.Today;
        public DateTime EndDate { get; set; } = DateTime.Today;
    }
}
