using MvvmCross.Commands;
using MvvmCross.Navigation;
using MvvmCross.ViewModels;

namespace Client.Core.ViewModels
{
    public class HomeViewModel : MvxViewModel
    {
        private readonly IMvxNavigationService navigationService;

        public IMvxCommand GoToAddVehicleCommand { get; set; }
        public IMvxCommand GoToEditVehicleCommand { get; set; }
        public IMvxCommand GoToDeleteVehicleCommand { get; set; }
        public IMvxCommand GoToAddRepairCommand { get; set; }
        public IMvxCommand GoToEmployeesCommand { get; set; }

        public HomeViewModel(IMvxNavigationService navigationService)
        {
            this.navigationService = navigationService;
            GoToAddVehicleCommand = new MvxCommand(() => navigationService.Navigate<AddVehicleViewModel>());
            GoToEditVehicleCommand = new MvxCommand(() => navigationService.Navigate<EditVehicleViewModel>());
            GoToDeleteVehicleCommand = new MvxCommand(() => navigationService.Navigate<DeleteVehicleViewModel>());
            GoToAddRepairCommand = new MvxCommand(() => navigationService.Navigate<AddRepairViewModel>());
            GoToEmployeesCommand = new MvxCommand(() => navigationService.Navigate<EmployeesViewModel>());
        }
    }
}
