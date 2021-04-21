using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Microsoft.Win32;
using OfficeOpenXml;

namespace Client.Wpf.Utils
{
    public static class Exporter
    {
        private const string separator = "\t";

        public static void ExportToCsv<TItem>(IList<string> headers, IList<TItem> items, IList<string> properties, Dictionary<string, string> metaData = null)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "CSV file (*.csv)|*.csv";

            if (saveFileDialog.ShowDialog().GetValueOrDefault())
            {
                StringBuilder stringBuilder = new StringBuilder();
                if (metaData != null)
                    foreach (var data in metaData)
                        stringBuilder.AppendLine($"{data.Key}{separator}{data.Value}");

                stringBuilder.AppendLine(string.Join(separator, headers));
                foreach (var item in items)
                    stringBuilder.AppendLine(GetJoinedValuesForItem(item, properties));

                using StreamWriter writer = new StreamWriter(saveFileDialog.FileName);
                writer.WriteLine(stringBuilder.ToString());
                writer.Close();
            }
        }

        public static void ExportToExcel<TItem>(IList<string> headers, IList<TItem> items, IList<string> properties, string title)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "Excel |*.xlsx";

            if (saveFileDialog.ShowDialog() == true)
            {
                using ExcelPackage excelPackage = new ExcelPackage();
                excelPackage.Workbook.Properties.Author = "Cars Manager";
                excelPackage.Workbook.Properties.Title = title;
                excelPackage.Workbook.Properties.Created = DateTime.Now;

                ExcelWorksheet worksheet = excelPackage.Workbook.Worksheets.Add(title);

                for (int i = 0; i < headers.Count; i++)
                {
                    worksheet.Cells[1, i + 1].Value = headers[i];
                    worksheet.Cells[1, i + 1].Style.Font.Bold = true;
                    worksheet.Cells[1, i + 1].Style.Border.Bottom.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thick;
                }

                for (int i = 0; i < items.Count; i++)
                {
                    var item = items[i];
                    for (int j = 0; j < properties.Count; j++)
                        worksheet.Cells[i + 2, j + 1].Value = typeof(TItem).GetProperty(properties[j]).GetValue(item);
                }

                worksheet.Cells.AutoFitColumns();

                FileInfo fi = new FileInfo(saveFileDialog.FileName);
                excelPackage.SaveAs(fi);
            }
        }

        private static string GetJoinedValuesForItem<TItem>(TItem item, IList<string> properties)
        {
            var values = properties.Select(h => typeof(TItem).GetProperty(h).GetValue(item)).ToArray();
            IList<string> stringedValues = new List<string>();
            foreach (var value in values)
            {
                if (value is DateTime)
                    stringedValues.Add(((DateTime)value).ToShortDateString());
                else if (value is bool)
                    stringedValues.Add(((bool)value) ? "Да" : "Не");
                else
                    stringedValues.Add(value?.ToString());
            }

            return string.Join(separator, stringedValues);
        }
    }
}

//private void ExportToExcel(object sender, RoutedEventArgs e)
//{
//    Task.Run(async () => await mileagesViewModel.GetAllVehiclesCommand.ExecuteAsync());

//    SaveFileDialog saveFileDialog = new SaveFileDialog();
//    saveFileDialog.Filter = "Excel |*.xlsx";

//    if (saveFileDialog.ShowDialog() == true)
//    {
//        using (ExcelPackage excelPackage = new ExcelPackage())
//        {
//            //Set some properties of the Excel document
//            excelPackage.Workbook.Properties.Author = "Car Manager";
//            excelPackage.Workbook.Properties.Title = "Пробези";
//            //excelPackage.Workbook.Properties.Subject = "EPPlus demo export data";
//            excelPackage.Workbook.Properties.Created = DateTime.Now;

//            //Create the WorkSheet
//            ExcelWorksheet worksheet = excelPackage.Workbook.Worksheets.Add("Пробези");

//            for (int i = 0; i < MileageGrid.Columns.Count; i++)
//            {
//                worksheet.Cells[1, i + 1].Value = MileageGrid.Columns[i].Header;
//                worksheet.Cells[1, i + 1].Style.Font.Bold = true;
//                worksheet.Cells[1, i + 1].Style.Font.Color.SetColor(Color.White);
//                worksheet.Cells[1, i + 1].Style.Border.Bottom.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thick;
//                worksheet.Cells[1, i + 1].Style.Border.Bottom.Color.SetColor(Color.Brown);
//                worksheet.Cells[1, i + 1].Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
//                worksheet.Cells[1, i + 1].Style.Fill.BackgroundColor.SetColor(0, 42, 96, 153);
//                worksheet.Column(i + 1).Width = 16;
//            }

//            //for (int i = 0; i < mileagesViewModel.Vehicles.Count; i++)
//            for (int i = 0; i < mileagesViewModel.AllVehicles.Count; i++)
//            {
//                var vehicle = mileagesViewModel.AllVehicles[i];
//                //var vehicle = mileagesViewModel.Vehicles[i];
//                worksheet.Cells[i + 2, 1].Value = vehicle.LicencePlate;
//                worksheet.Cells[i + 2, 2].Value = vehicle.Make;
//                worksheet.Cells[i + 2, 3].Value = vehicle.Model;
//                worksheet.Cells[i + 2, 4].Value = vehicle.Color;
//                worksheet.Cells[i + 2, 5].Value = vehicle.Mileage;
//                worksheet.Cells[i + 2, 5].Style.Font.Bold = true;
//                worksheet.Cells[i + 2, 5].Style.Font.Color.SetColor(0, 255, 0, 0);

//                if (i % 2 != 0)
//                {
//                    worksheet.Cells[i + 2, 1].Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
//                    worksheet.Cells[i + 2, 1].Style.Fill.BackgroundColor.SetColor(0, 255, 233, 148);
//                    worksheet.Cells[i + 2, 1].Style.Border.Bottom.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
//                    worksheet.Cells[i + 2, 1].Style.Border.Bottom.Color.SetColor(Color.Brown);

//                    worksheet.Cells[i + 2, 2].Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
//                    worksheet.Cells[i + 2, 2].Style.Fill.BackgroundColor.SetColor(0, 255, 233, 148);
//                    worksheet.Cells[i + 2, 2].Style.Border.Bottom.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
//                    worksheet.Cells[i + 2, 2].Style.Border.Bottom.Color.SetColor(Color.Brown);

//                    worksheet.Cells[i + 2, 3].Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
//                    worksheet.Cells[i + 2, 3].Style.Fill.BackgroundColor.SetColor(0, 255, 233, 148);
//                    worksheet.Cells[i + 2, 3].Style.Border.Bottom.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
//                    worksheet.Cells[i + 2, 3].Style.Border.Bottom.Color.SetColor(Color.Brown);

//                    worksheet.Cells[i + 2, 4].Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
//                    worksheet.Cells[i + 2, 4].Style.Fill.BackgroundColor.SetColor(0, 255, 233, 148);
//                    worksheet.Cells[i + 2, 4].Style.Border.Bottom.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
//                    worksheet.Cells[i + 2, 4].Style.Border.Bottom.Color.SetColor(Color.Brown);

//                    worksheet.Cells[i + 2, 5].Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
//                    worksheet.Cells[i + 2, 5].Style.Fill.BackgroundColor.SetColor(0, 255, 233, 148);
//                    worksheet.Cells[i + 2, 5].Style.Border.Bottom.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
//                    worksheet.Cells[i + 2, 5].Style.Border.Bottom.Color.SetColor(Color.Brown);
//                }
//            }

//            FileInfo fi = new FileInfo(saveFileDialog.FileName);
//            excelPackage.SaveAs(fi);
//        }
//    }
//}
