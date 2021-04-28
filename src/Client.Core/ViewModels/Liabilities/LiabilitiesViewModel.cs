using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Client.Core.Data;
using Client.Core.Dtos;
using Client.Core.Models.Liabilities;
using Client.Core.Rest;
using Client.Core.Services;
using Client.Core.ViewModels.Common;
using MvvmCross.Navigation;

namespace Client.Core.ViewModels.Liabilities
{
    public class LiabilitiesViewModel : ReportViewModel<LiabilityForReportModel, LiabilityType>
    {
        private IList<string> properties = new List<string>
        {
            nameof(LiabilityForReportModel.LicencePlate),
            nameof(LiabilityForReportModel.MakeAndModel),
            nameof(LiabilityForReportModel.VehicleTypeName),
            nameof(LiabilityForReportModel.Color),
            nameof(LiabilityForReportModel.StartDate),
            nameof(LiabilityForReportModel.EndDate),
            nameof(LiabilityForReportModel.RemainingDays),
        };

        private LiabilityType liabilityType;

        private string controller => liabilityType switch
        {
            LiabilityType.MOT => "mots",
            LiabilityType.CivilLiability => "civilliabilities",
            LiabilityType.CarInsurance => "carinsurances",
            LiabilityType.Vignette => "vignettes",
            _ => throw new ArgumentException($"Invalid liability type: {liabilityType}"),
        };

        protected override IList<string> Properties => properties;

        public string Caption => liabilityType switch
        {
            LiabilityType.MOT => "Справка \"Годишен технически преглед\"",
            LiabilityType.CivilLiability => "Справка \"Застраховка гражданска отговорност\"",
            LiabilityType.CarInsurance => "Справка \"Застраховка КАСКО\"",
            LiabilityType.Vignette => "Справка \"Винетки\"",
            _ => string.Empty,
        };

        public LiabilitiesViewModel(IApiService apiService, IMvxNavigationService navigationService, ICurrentUserService currentUserService)
            : base(apiService, navigationService, currentUserService)
        {
        }

        public override void Prepare(LiabilityType parameter)
        {
            liabilityType = parameter;
        }

        protected override async Task GetAllItems()
        {
            var response = await ApiService.GetAsync<IList<LiabilityForReportModel>>($"{controller}/reports/all");

            if (response.IsSuccessStatusCode)
            {
                var liabilities = response.Content;
                foreach (var liability in liabilities)
                    liability.VehicleTypeName = VehicleResources.GetVehicleType(liability.VehicleType);

                AllItems = liabilities;
            }
            else
                RaiseNotification(response.Error, "Грешка!!!");
        }

        protected override async Task GetItems(int pageNumber = 1)
        {
            var response = await ApiService.GetAsync<PaginatedListDto<LiabilityForReportModel>>($"{controller}/reports/pages?pagenumber={pageNumber}");

            if (response.IsSuccessStatusCode)
                PageData = response.Content;
            else
                RaiseNotification(response.Error, "Грешка!!!");
        }
    }
}
