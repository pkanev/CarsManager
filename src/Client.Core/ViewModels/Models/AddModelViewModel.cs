using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Client.Core.Data;
using Client.Core.Models;
using Client.Core.Rest;
using Client.Core.ViewModels.Common;
using MvvmCross.Commands;
using MvvmCross.Navigation;

namespace Client.Core.ViewModels.Models
{
    public class AddModelViewModel : BaseViewModel<NavigationModel<MakeModel>>
    {
        private ModelModel model = new ModelModel();
        private Func<Task> onAddComplete;

        public ModelModel Model
        {
            get => model;
            set => SetProperty(ref model, value);
        }

        public string Name
        {
            get => Model.Name;
            set
            {
                Model.Name = value;
                RaisePropertyChanged(() => CanSave);
            }
        }

        public VehicleTypeModel VehicleType
        {
            get => VehicleResources.VehicleTypes[Model.VehicleType];
            set => Model.VehicleType = value.Id;
        }

        public bool CanSave => !string.IsNullOrWhiteSpace(Name);

        public List<VehicleTypeModel> VehicleTypes => VehicleResources.VehicleTypes;
        public IMvxCommand CreateModelCommand { get; set; }

        public AddModelViewModel(IApiService apiService, IMvxNavigationService navigationService)
            : base(apiService, navigationService)
        {
            CreateModelCommand = new MvxAsyncCommand(CreateModel);
        }

        public override void Prepare(NavigationModel<MakeModel> model)
        {
            Model.MakeId = model.Data.Id;
            Model.Make = model.Data.Name;
            onAddComplete = model.Callback;
        }

        private async Task CreateModel()
        {
            Model.Name = Name;
            Model.VehicleType = VehicleType.Id;

            var result = await ApiService.PostAsync<int>("models", Model);

            if (result.IsSuccessStatusCode)
            {
                await onAddComplete();
                await NavigationService.Close(this);
            }
            else
                RaiseNotification(result.Error, "Грешка!!!");
        }
    }
}
