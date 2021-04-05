using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Client.Core.Dtos;
using Client.Core.Rest;
using Client.Core.Utils;
using MvvmCross.Commands;
using MvvmCross.Navigation;

namespace Client.Core.ViewModels
{
    public class DeleteVehicleViewModel : SubPageViewModel
    {
        private ObservableCollection<VehicleDto> vehicles;
        private VehicleDto vehicle;

        public ObservableCollection<VehicleDto> Vehicles
        {
            get => vehicles;
            set => SetProperty(ref vehicles, value);
        }

        public VehicleDto Vehicle
        {
            get => vehicle;
            set
            {
                SetProperty(ref vehicle, value);
                RaisePropertyChanged(() => CanDelete);
            }
        }

        public bool CanDelete => Vehicle != null;

        public IMvxCommand DeleteVehicleCommand { get; set; }

        public DeleteVehicleViewModel(IApiService apiService, IMvxNavigationService navigationService)
            : base(apiService, navigationService)
        {
            DeleteVehicleCommand = new MvxAsyncCommand(DeleteVehicle);
        }

        public override Task Initialize()
        {
            GetVehicles();
            return base.Initialize();
        }

        private async Task GetVehicles()
        {
            var response = await ApiService.GetAsync<GetVehiclesDto>("vehicles");
            if (response.IsSuccessStatusCode)
                Vehicles = response.Content.Vehicles.ToObservableCollection();
            else
                RaiseNotification(response.Error, "Грешка!!!");
        }

        private async Task DeleteVehicle()
            => await ShowMessage(
                $"Сигурни ли сте, че желаете да изтриете автомобил с номер \"{Vehicle.LicencePlate}\" от базата?",
                "Изтриване на автомобил",
                OnDeleteConfirm);

        private async Task OnDeleteConfirm()
        {
            var result = await ApiService.DeleteAsync<string>($"vehicles/{Vehicle.Id}");

            if (result.IsSuccessStatusCode)
                Vehicles.Remove(Vehicle);
            else
                RaiseNotification(result.Error, "Грешка!!!");
        }
    }
}
