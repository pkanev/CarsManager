namespace Client.Core.Models.Repairs
{
    public class RepairModel : BasicRepairModel
    {
        public bool IsOilChanged { get; set; }
        public bool IsBeltChanged { get; set; }
        public bool IsBatteryChanged { get; set; }
        public bool IsSparkPlugChanged { get; set; }
        public bool IsFuelFilterChanged { get; set; }
        public bool IsBrakeLiningsChanged { get; set; }
        public bool IsBrakeDisksChanged { get; set; }
        public bool IsCoolantChanged { get; set; }
        public bool IsOtherWorkDone { get; set; }
        public decimal FinalPrice { get; set; }
    }
}
