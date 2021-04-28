using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using Client.Core.Dtos;
using Client.Core.Models.RoadBook;
using Client.Core.Models.Vehicles;
using Client.Core.Rest;
using Client.Core.Services;
using Client.Core.Utils;
using Client.Core.ViewModels.Common;
using MvvmCross.Commands;
using MvvmCross.Navigation;

namespace Client.Core.ViewModels.RoadBook
{
    public class RoadBookViewModel : ReportViewModel<RoadBookEntryModel>
    {
        private List<string> properties = new List<string>
        {
            nameof(RoadBookEntryModel.CheckedOut),
            nameof(RoadBookEntryModel.MakeAndModel),
            nameof(RoadBookEntryModel.Color),
            nameof(RoadBookEntryModel.OldMileage),
            nameof(RoadBookEntryModel.CheckedIn),
            nameof(RoadBookEntryModel.NewMileage),
            nameof(RoadBookEntryModel.Drivers),
            nameof(RoadBookEntryModel.Destination),
            nameof(RoadBookEntryModel.IsFullyCheckedIn),
        };

        private DateTime from;
        private DateTime to;
        private BasicVehicleModel vehicle;
        private ObservableCollection<BasicVehicleModel> vehicles;

        protected override IList<string> Properties => properties;

        public DateTime From
        {
            get => from;
            set
            {
                SetProperty(ref from, value);
                RaisePropertyChanged(() => CanReload);
            }
        }

        public DateTime To
        {
            get => to;
            set
            {
                SetProperty(ref to, value);
                RaisePropertyChanged(() => CanReload);
            }
        }

        public BasicVehicleModel Vehicle
        {
            get => vehicle;
            set
            {
                SetProperty(ref vehicle, value);
                Task.Run(async () => await GetItems());
            }
        }

        public ObservableCollection<BasicVehicleModel> Vehicles
        {
            get => vehicles;
            set => SetProperty(ref vehicles, value);
        }

        public bool CanReload => Vehicle != null && From < To;

        public IMvxCommand ReloadCommand { get; private set; }

        public RoadBookViewModel(IApiService apiService, IMvxNavigationService navigationService, ICurrentUserService currentUserService)
            : base(apiService, navigationService, currentUserService)
        {
            var now = DateTime.Now;
            From = new DateTime(now.Year, now.Month, 1);
            To = From.AddMonths(1).AddDays(-1);

            ReloadCommand = new MvxAsyncCommand(() => GetItems(1));
        }

        public override async Task Initialize()
        {
            await GetAllVehicles();
        }

        private async Task GetAllVehicles()
        {
            var response = await ApiService.GetAsync<IList<BasicVehicleModel>>("vehicles/basic");

            if (response.IsSuccessStatusCode)
            {
                Vehicles = response.Content.ToObservableCollection();
                Vehicle = Vehicles.FirstOrDefault();
            }
            else
                RaiseNotification(response.Error, "Грешка!!!");
        }

        protected override async Task GetAllItems()
        {
            if (Vehicle == null)
                return;

            var queryParams = $"VehicleId={Vehicle.Id}&From={From.ToString("d", CultureInfo.InvariantCulture)}&To={To.ToString("d", CultureInfo.InvariantCulture)}";
            var response = await ApiService.GetAsync<IList<RoadBookEntryModel>>($"roadbookentries?{queryParams}");

            if (response.IsSuccessStatusCode)
                AllItems = response.Content;
            else
                RaiseNotification(response.Error, "Грешка!!!");
        }

        protected override async Task GetItems(int pageNumber = 1)
        {
            if (Vehicle == null)
                return;

            var queryParams = $"pageNumber={pageNumber}&vehicleId={Vehicle.Id}&from={From.ToString("d", CultureInfo.InvariantCulture)}&to={To.ToString("d", CultureInfo.InvariantCulture)}";
            var response = await ApiService.GetAsync<PaginatedListDto<RoadBookEntryModel>>($"roadbookentries/pages?{queryParams}");

            if (response.IsSuccessStatusCode)
                PageData = response.Content;
            else
                RaiseNotification(response.Error, "Грешка!!!");
        }
    }
}
