using System.Linq;
using System.Threading.Tasks;
using Client.Core.Models;
using Client.Core.Rest;
using Client.Core.Utils;
using MvvmCross.Commands;
using MvvmCross.Navigation;

namespace Client.Core.ViewModels
{
    public class AddLiabilityViewModel : BaseLiabilityViewModel
    {
        public override string Caption => Liability.LiabilityType switch
        {
            LiabilityType.MOT => "Нов годишен техничекси преглед",
            LiabilityType.CivilLiability => "Нова застраховка \"ГО\"",
            LiabilityType.CarInsurance => "Нова застраховка \"КАСКО\"",
            LiabilityType.Vignette => "Нова винетка",
            _ => string.Empty
        };

        public AddLiabilityViewModel(IApiService apiService, IMvxNavigationService navigationService)
            : base(apiService, navigationService)
        {
            SaveCommand = new MvxAsyncCommand(Save);
            CancelCommand = new MvxCommand(() => NavigationService.Close(this));
        }

        public override void Prepare(NavigationModel<LiabilityExtendedModel> model)
        {
            base.Prepare(model);
            Period = Periods.FirstOrDefault();
        }

        protected override async Task Save()
        {
            Liability.EndDate = Liability.StartDate.AddValidityPeriod(Period);

            var response = await ApiService.PostAsync<int>($"{Controller}", Liability);

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
