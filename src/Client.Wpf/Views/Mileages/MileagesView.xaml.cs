using System.Linq;
using System.Windows;
using System.Windows.Controls;
using Client.Core.Models;
using Client.Core.Models.Vehicles;
using Client.Core.ViewModels.Mileages;
using Client.Wpf.Utils;
using Client.Wpf.Views.Common;
using MvvmCross.Base;

namespace Client.Wpf.Views.Mileages
{
    public partial class MileagesView : ReportView<BasicVehicleModel>
    {
        private MileagesViewModel mileagesViewModel => ViewModel as MileagesViewModel;

        public MileagesView()
        {
            InitializeComponent();
        }

        protected override void OnInitialized(System.EventArgs e)
        {
            base.OnInitialized(e);
            MileageGrid.CellEditEnding += resultGrid_CellEditEnding;
        }

        void resultGrid_CellEditEnding(object sender, DataGridCellEditEndingEventArgs e)
        {
            var vehicle = e.EditingElement.DataContext as BasicVehicleModel;
            var editingTextBox = e.EditingElement as TextBox;
            int newMileage;
            if (int.TryParse(editingTextBox.Text, out newMileage) && newMileage != vehicle.Mileage)
                mileagesViewModel.SaveMileageCommand.Execute((vehicle.Id, newMileage));
        }

        private void PrintPage(object sender, RoutedEventArgs e)
            => Printer.PrintWpfPreview(PageContent);

        protected override void OnExportInteractionRequested(object sender, MvxValueEventArgs<ExportModel<BasicVehicleModel>> eventArgs)
        {
            var model = eventArgs.Value;
            var headers = MileageGrid.Columns.Select(c => c.Header.ToString()).ToList();
            switch (model.ExportType)
            {
                case Core.Data.ExportType.Csv:
                    Exporter.ExportToCsv(headers, model.Items, model.Properties, ViewTitle.Text);
                    return;
                case Core.Data.ExportType.Excel:
                    Exporter.ExportToExcel(headers, model.Items, model.Properties, ViewTitle.Text);
                    return;
                default:
                    break;
            }
        }
    }
}
