using Client.Core.ViewModels.Vehicles;
using MvvmCross.Binding.BindingContext;

namespace Client.Wpf.Views.Vehicles
{
    public partial class AddVehicleView : BaseVehicleView
    {
        public AddVehicleView()
        {
            InitializeComponent();

            var addVehicleBindings = this.CreateBindingSet<AddVehicleView, AddVehicleViewModel>();
            addVehicleBindings.Bind(this).For(view => view.UploadInteraction).To(viewModel => viewModel.UploadInteraction).OneWay();
            addVehicleBindings.Apply();
        }
    }
}
