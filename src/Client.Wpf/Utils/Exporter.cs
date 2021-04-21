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

        public static void ExportToCsv<TItem>(IList<string> headers, IList<TItem> items, IList<string> properties, string title, Dictionary<string, string> metaData = null)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.FileName = SanitizeFileName(title);
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

        public static void ExportToExcel<TItem>(IList<string> headers, IList<TItem> items, IList<string> properties, string title, Dictionary<string, string> metaData = null)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.FileName = SanitizeFileName(title);
            saveFileDialog.Filter = "Excel |*.xlsx";

            if (saveFileDialog.ShowDialog() == true)
            {
                using ExcelPackage excelPackage = new ExcelPackage();
                excelPackage.Workbook.Properties.Author = "Cars Manager";
                excelPackage.Workbook.Properties.Title = title;
                excelPackage.Workbook.Properties.Created = DateTime.Now;

                ExcelWorksheet worksheet = excelPackage.Workbook.Worksheets.Add(title);

                int row = 1;
                if (metaData != null)
                    foreach(var data in metaData)
                    {
                        worksheet.Cells[row, 1].Value = data.Key;
                        worksheet.Cells[row, 1].Style.Font.Bold = true;
                        worksheet.Cells[row, 2].Value = data.Value;
                        row++;
                    }

                for (int i = 0; i < headers.Count; i++)
                {
                    worksheet.Cells[row, i + 1].Value = headers[i];
                    worksheet.Cells[row, i + 1].Style.Font.Bold = true;
                    worksheet.Cells[row, i + 1].Style.Border.Bottom.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thick;
                }

                row++;

                for (int i = 0; i < items.Count; i++)
                {
                    var item = items[i];
                    for (int j = 0; j < properties.Count; j++)
                        worksheet.Cells[row, j + 1].Value = GetValueAsString(
                            typeof(TItem).GetProperty(properties[j]).GetValue(item));

                    row++;
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
                stringedValues.Add(GetValueAsString(value));

            return string.Join(separator, stringedValues);
        }

        private static string GetValueAsString(object value)
        {
            if (value is DateTime)
                return ((DateTime)value).ToShortDateString();
            else if (value is bool)
                return ((bool)value) ? "Да" : "Не";
            else
                return value?.ToString();
        }

        private static string SanitizeFileName(string title)
        {
            var result = title.Replace(",", string.Empty);
            result = result.Replace("\"", string.Empty);
            result = result.Replace("\'", string.Empty);
            return result;
        }
    }
}
