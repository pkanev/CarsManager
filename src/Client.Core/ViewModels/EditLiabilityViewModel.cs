using System.Threading.Tasks;
using Client.Core.Models;
using Client.Core.Models.Liabilities;
using Client.Core.Rest;
using Client.Core.Utils;
using MvvmCross.Navigation;

namespace Client.Core.ViewModels
{
    public class EditLiabilityViewModel : BaseLiabilityViewModel
    {
        public override string Caption => Liability.LiabilityType switch
        {
            LiabilityType.MOT => "Промяна на техничекси преглед",
            LiabilityType.CivilLiability => "Промяна на застраховка \"ГО\"",
            LiabilityType.CarInsurance => "Промяна на застраховка \"КАСКО\"",
            LiabilityType.Vignette => "Промяна на винетка",
            _ => string.Empty
        };

        public EditLiabilityViewModel(IApiService apiService, IMvxNavigationService navigationService)
            : base(apiService, navigationService)
        {
        }

        public override void Prepare(NavigationModel<LiabilityExtendedModel> model)
        {
            base.Prepare(model);
            foreach (var period in Periods)
            {
                var endDate = Liability.StartDate.AddValidityPeriod(period);
                if (endDate == Liability.EndDate)
                {
                    Period = period;
                    return;
                }
            }
        }

        protected override async Task Save()
        {
            Liability.EndDate = Liability.StartDate.AddValidityPeriod(Period);

            var response = await ApiService.PutAsync<int>($"{Controller}?id={Liability.Id}", Liability);

            if (response.IsSuccessStatusCode)
            {
                await OnSave();
                await NavigationService.Close(this);
            }
            else
                RaiseNotification(response.Error, "Грешка!!!");
        }
    }
}
