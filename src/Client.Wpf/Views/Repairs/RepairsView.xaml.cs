using System.Collections.Generic;
using System.Linq;
using System.Windows;
using Client.Core.Models;
using Client.Core.Models.Repairs;
using Client.Core.Models.Vehicles;
using Client.Wpf.Utils;
using Client.Wpf.Views.Common;
using MvvmCross.Base;

namespace Client.Wpf.Views.Repairs
{
    public partial class RepairsView : ReportView<BasicRepairModel>
    {
        public RepairsView()
        {
            InitializeComponent();
        }

        private void PrintPage(object sender, RoutedEventArgs e)
            => Printer.PrintWpfPreview(PageContent);

        protected override void OnExportInteractionRequested(object sender, MvxValueEventArgs<ExportModel<BasicRepairModel>> eventArgs)
        {
            var model = eventArgs.Value;
            var headers = ReportDataGrid.Columns.Select(c => c.Header.ToString()).ToList();
            var metaData = new Dictionary<string, string>();
            metaData[VehicleLabel.Text] = (VehiclesCombo.SelectedItem as BasicVehicleModel).LicencePlate;
            switch (model.ExportType)
            {
                case Core.Data.ExportType.Csv:
                    Exporter.ExportToCsv(headers, model.Items, model.Properties, ViewTitle.Text, metaData);
                    return;
                case Core.Data.ExportType.Excel:
                    Exporter.ExportToExcel(headers, model.Items, model.Properties, ViewTitle.Text, metaData);
                    return;
                default:
                    break;
            }
        }
    }
}
