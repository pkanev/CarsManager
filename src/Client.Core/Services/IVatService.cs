namespace Client.Core.Services
{
    public interface IVatService
    {
        public decimal GetBasePrice(decimal finalPrice);
        public string GetFormattedBasePrice(decimal finalPrice, string format = "#.##");
        public string GetFormattedVat(decimal finalPrice, string format = "#.##");
    }
}
