using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using Client.Core.Dtos;
using Client.Core.Models.Repairs;
using Client.Core.Models.Vehicles;
using Client.Core.Rest;
using Client.Core.Utils;
using Client.Core.ViewModels.Common;
using MvvmCross.Navigation;

namespace Client.Core.ViewModels
{
    public class RepairsViewModel : ReportViewModel<BasicRepairModel>
    {
        private IList<string> properties = new List<string>
        {
            nameof(BasicRepairModel.Date),
            nameof(BasicRepairModel.Description),
            nameof(BasicRepairModel.Mileage),
            nameof(BasicRepairModel.RepairShopName)
        };

        private BasicVehicleModel selectedVehicle;
        private ObservableCollection<BasicVehicleModel> vehicles;

        public BasicVehicleModel SelectedVehicle
        {
            get => selectedVehicle;
            set
            {
                SetProperty(ref selectedVehicle, value);
                Task.Run(async() => await GetItems());
            }
        }

        public ObservableCollection<BasicVehicleModel> Vehicles
        {
            get => vehicles;
            set => SetProperty(ref vehicles, value);
        }

        protected override IList<string> Properties => properties;

        public RepairsViewModel(IApiService apiService, IMvxNavigationService navigationService)
            : base(apiService, navigationService)
        {
        }

        public override async Task Initialize() => await GetVehicles();

        protected override async Task GetItems(int pageNumber = 1)
        {
            var response = await ApiService.GetAsync<PaginatedListDto<BasicRepairModel>> ($"repairs/vehicle/{SelectedVehicle.Id}/pages?pagenumber={pageNumber}");

            if (response.IsSuccessStatusCode)
                PageData = response.Content;
            else
                RaiseNotification(response.Error, "Грешка!!!");
        }

        protected override async Task GetAllItems()
        {
            var response = await ApiService.GetAsync<IList<BasicRepairModel>>($"repairs/vehicle/{SelectedVehicle.Id}/basic");

            if (response.IsSuccessStatusCode)
                AllItems = response.Content;
            else
                RaiseNotification(response.Error, "Грешка!!!");
        }

        private async Task GetVehicles()
        {
            var response = await ApiService.GetAsync<IList<BasicVehicleModel>>($"vehicles/basic");

            if (response.IsSuccessStatusCode)
            {
                Vehicles = response.Content.ToObservableCollection();
                SelectedVehicle = Vehicles.FirstOrDefault();
            }
            else
                RaiseNotification(response.Error, "Грешка!!!");
        }
    }
}
