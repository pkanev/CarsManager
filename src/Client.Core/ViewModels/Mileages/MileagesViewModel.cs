using System.Collections.Generic;
using System.Threading.Tasks;
using Client.Core.Dtos;
using Client.Core.Models.Vehicles;
using Client.Core.Rest;
using Client.Core.ViewModels.Common;
using MvvmCross.Commands;
using MvvmCross.Navigation;

namespace Client.Core.ViewModels.Mileages
{
    public class MileagesViewModel : ReportViewModel<BasicVehicleModel>
    {
        private IList<string> properties = new List<string>
        {
            nameof(BasicVehicleModel.LicencePlate),
            nameof(BasicVehicleModel.Make),
            nameof(BasicVehicleModel.Model),
            nameof(BasicVehicleModel.Color),
            nameof(BasicVehicleModel.Mileage),
        };

        public IMvxCommand<(int, int)> SaveMileageCommand { get; set; }

        protected override IList<string> Properties => properties;

        public MileagesViewModel(IApiService apiService, IMvxNavigationService navigationService)
            : base(apiService, navigationService)
        {
            SaveMileageCommand = new MvxAsyncCommand<(int, int)>(SaveMileage);
        }

        private async Task SaveMileage((int, int) values)
        {
            int id = values.Item1;
            int mileage = values.Item2;

            var response = await ApiService.PatchAsync<string>($"vehicles/{id}", new { VehicleId = id, Mileage = mileage });

            if (!response.IsSuccessStatusCode)
                RaiseNotification(response.Error, "Грешка!!!");
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
            var response = await ApiService.GetAsync<IList<BasicVehicleModel>>("vehicles/basic");

            if (response.IsSuccessStatusCode)
                AllItems = response.Content;
            else
                RaiseNotification(response.Error, "Грешка!!!");
        }
    }
}
