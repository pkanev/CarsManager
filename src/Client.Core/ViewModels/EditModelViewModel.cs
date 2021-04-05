using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Client.Core.Data;
using Client.Core.Models;
using Client.Core.Rest;
using MvvmCross.Commands;
using MvvmCross.Navigation;

namespace Client.Core.ViewModels
{
    public class EditModelViewModel : BaseViewModel<NavigationModel<ModelModel>>
    {
        private ModelModel model;
        private Func<Task> onEditComplete;

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
            get => VehicleTypes[Model.VehicleType];
            set => Model.VehicleType = value.Id;
        }

        public bool CanSave => !string.IsNullOrWhiteSpace(Model.Name);
        public List<VehicleTypeModel> VehicleTypes => VehicleResources.VehicleTypes;

        public IMvxCommand EditModelCommand { get; set; }

        public EditModelViewModel(IApiService apiService, IMvxNavigationService navigationService)
            : base(apiService, navigationService)
        {
            EditModelCommand = new MvxAsyncCommand(EditModel);
        }

        public override void Prepare(NavigationModel<ModelModel> model)
        {
            Model = model.Data;
            onEditComplete = model.Callback;
        }

        private async Task EditModel()
        {
            var result = await ApiService.PutAsync<string>($"models?id={Model.Id}", Model);

            if (result.IsSuccessStatusCode)
            {
                await onEditComplete();
                await NavigationService.Close(this);
            }
            else
                RaiseNotification(result.Error, "Грешка!!!", () => NavigationService.Close(this));
        }
    }
}
