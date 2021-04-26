using Client.Core.ViewModels.Vehicles;
using MvvmCross.Binding.BindingContext;

namespace Client.Wpf.Views.Vehicles
{
    public partial class EditVehicleView : BaseVehicleView
    {
        public EditVehicleView()
        {
            InitializeComponent();

            var set = this.CreateBindingSet<EditVehicleView, EditVehicleViewModel>();
            set.Bind(this).For(view => view.UploadInteraction).To(viewModel => viewModel.UploadInteraction).OneWay();
            set.Apply();
        }
    }
}
