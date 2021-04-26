using System.Linq;
using System.Windows;
using Client.Core.Models;
using Client.Core.Models.Vehicles;
using Client.Wpf.Utils;
using Client.Wpf.Views.Common;
using MvvmCross.Base;

namespace Client.Wpf.Views.Vehicles
{
    public partial class VehiclesView : ReportView<BasicVehicleModel>
    {
        public VehiclesView()
        {
            InitializeComponent();
        }

        private void PrintPage(object sender, RoutedEventArgs e)
            => Printer.PrintWpfPreview(PageContent);

        protected override void OnExportInteractionRequested(object sender, MvxValueEventArgs<ExportModel<BasicVehicleModel>> eventArgs)
        {
            var model = eventArgs.Value;
            var headers = ReportDataGrid.Columns.Select(c => c.Header.ToString()).ToList();
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
