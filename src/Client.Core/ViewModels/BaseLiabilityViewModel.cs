using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Client.Core.Data;
using Client.Core.Models;
using Client.Core.Models.Liabilities;
using Client.Core.Rest;
using Client.Core.Utils;
using MvvmCross.Commands;
using MvvmCross.Navigation;

namespace Client.Core.ViewModels
{
    public abstract class BaseLiabilityViewModel : BaseViewModel<NavigationModel<LiabilityExtendedModel>>
    {
        private LiabilityExtendedModel liability = new LiabilityExtendedModel();
        private Func<Task> onSave;
        private ValidityPeriod period;
        private ObservableCollection<ValidityPeriod> periods;

        public abstract string Caption { get; }

        public LiabilityExtendedModel Liability
        {
            get => liability;
            set
            {
                SetProperty(ref liability, value);
                RaisePropertyChanged(() => CanSave);
            }
        }

        public DateTime StartDate
        {
            get => Liability.StartDate;
            set
            {
                Liability.StartDate = value;
                RaisePropertyChanged(() => StartDate);
                RaisePropertyChanged(() => CanSave);
            }
        }


        public ValidityPeriod Period
        {
            get => period;
            set
            {
                SetProperty(ref period, value);
                RaisePropertyChanged(() => CanSave);
            }
        }

        public ObservableCollection<ValidityPeriod> Periods
        {
            get => periods;
            set => SetProperty(ref periods, value);
        }

        public bool CanSave => Period != null && Liability.StartDate != default(DateTime);

        protected string Controller => LiabilityResources.GetLiabilityController(Liability.LiabilityType);

        protected Func<Task> OnSave => onSave;

        public IMvxCommand CancelCommand { get; set; }
        public IMvxCommand SaveCommand { get; set; }

        protected BaseLiabilityViewModel(IApiService apiService, IMvxNavigationService navigationService)
            : base(apiService, navigationService)
        {
            SaveCommand = new MvxAsyncCommand(Save);
            CancelCommand = new MvxCommand(() => NavigationService.Close(this));
        }

        public override void Prepare(NavigationModel<LiabilityExtendedModel> model)
        {
            Liability = model.Data;
            onSave = model.Callback;
            Periods = Liability.LiabilityType switch
            {
                LiabilityType.MOT => ValidityPeriods.MotValidityPeriods.ToObservableCollection(),
                LiabilityType.CivilLiability => ValidityPeriods.CivilLiabilityValidityPeriods.ToObservableCollection(),
                LiabilityType.CarInsurance => ValidityPeriods.CarInsurancePeriodValidityPeriods.ToObservableCollection(),
                LiabilityType.Vignette => ValidityPeriods.VignetteValidityPeriods.ToObservableCollection(),
                _ => ValidityPeriods.MotValidityPeriods.ToObservableCollection(),
            };
        }

        protected abstract Task Save();
    }
}
