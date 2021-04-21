using System;

namespace Client.Core.Services
{
    public class VatService : IVatService
    {
        private decimal vatRate => Properties.Settings.Default.VAT / 100;

        public decimal GetBasePrice(decimal finalPrice)
            => Math.Round(finalPrice / (1 + vatRate), 2);

        public string GetFormattedBasePrice(decimal finalPrice, string format = "#.##")
            => GetBasePrice(finalPrice).ToString(format);

        public string GetFormattedVat(decimal finalPrice, string format = "#.##")
            => (finalPrice - GetBasePrice(finalPrice)).ToString(format);
    }
}
