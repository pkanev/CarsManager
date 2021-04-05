using Client.Core.ViewModels;
using MvvmCross.Binding.BindingContext;

namespace Client.Wpf.Views
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
