using System.Collections.Generic;
using System.Threading.Tasks;
using Client.Core.Dtos;
using Client.Core.Models.Vehicles;
using Client.Core.Rest;
using MvvmCross.Navigation;

namespace Client.Core.ViewModels
{
    public class VehiclesViewModel : ReportViewModel<BasicVehicleModel>
    {
        private IList<string> properties = new List<string>
        {
            nameof(BasicVehicleModel.LicencePlate),
            nameof(BasicVehicleModel.Make),
            nameof(BasicVehicleModel.Model),
            nameof(BasicVehicleModel.Color),
            nameof(BasicVehicleModel.Mileage),
            nameof(BasicVehicleModel.IsCheckedOut),
        };

        protected override IList<string> Properties => properties;

        public VehiclesViewModel(IApiService apiService, IMvxNavigationService navigationService)
            : base(apiService, navigationService)
        {
        }

        protected override async Task GetItems(int pageNumber = 1)
        {
            var response = await ApiService.GetAsync<PaginatedListDto<BasicVehicleModel>>($"vehicles/pages?pagenumber={pageNumber}");

            if (response.IsSuccessStatusCode)
                PageData = response.Content;
            else
                RaiseNotification(response.Error, "Грешка!!!");
        }

        protected override async Task GetAllItems()
        {
            var response = await ApiService.GetAsync<IList<BasicVehicleModel>>($"vehicles/basic");

            if (response.IsSuccessStatusCode)
                AllItems = response.Content;
            else
                RaiseNotification(response.Error, "Грешка!!!");
        }
    }
}
