using System.IO;
using System.Printing;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Documents.Serialization;
using System.Windows.Xps;
using System.Windows.Xps.Packaging;
using Client.Wpf.Views;

namespace Client.Wpf.Utils
{
    public static class Printer
    {
        public static void PrintWpfPreview(FrameworkElement element)
        {
            string printFileName = "print_preview.xps";
            if (File.Exists(printFileName))
                File.Delete(printFileName);

            //System.Printing.PrintDocumentImageableArea ia = null;
            using XpsDocument doc = new XpsDocument(printFileName, FileAccess.ReadWrite);

            XpsDocumentWriter writer = XpsDocument.CreateXpsDocumentWriter(doc);

            SerializerWriterCollator outputDocument = writer.CreateVisualsCollator();
            outputDocument.BeginBatchWrite();
            outputDocument.Write(element);
            outputDocument.EndBatchWrite();

            FixedDocumentSequence preview = doc.GetFixedDocumentSequence();
            DocumentPaginator paginator = preview.DocumentPaginator;
            preview.DocumentPaginator.PageSize = new Size(200, 100);
            var a = preview.DocumentPaginator.PageCount;
            PrintWindow printWindow = new PrintWindow(preview);
            printWindow.Show();

            doc.Close();
            writer = null;
            outputDocument = null;
        }

        public static void Print(FrameworkElement element)
        {
            var printDialog = new PrintDialog();

            var sz = new Size(printDialog.PrintableAreaWidth, printDialog.PrintableAreaHeight);

            // create paginator
            //var paginator = new FlexPaginator(_flex, ScaleMode.PageWidth, sz, new Thickness(96 / 4), 100);
            string tempFileName = System.IO.Path.GetTempFileName();

            File.Delete(tempFileName);

            using (XpsDocument xpsDocument = new XpsDocument(tempFileName, FileAccess.ReadWrite))
            {
                XpsDocumentWriter writer = XpsDocument.CreateXpsDocumentWriter(xpsDocument);
                writer.Write(element);
                DocumentViewer previewWindow = new DocumentViewer
                {
                    Document = xpsDocument.GetFixedDocumentSequence()
                };

                Window printpriview = new Window();
                printpriview.Content = previewWindow;
                printpriview.Title = "Разпечатка на страница";
                printpriview.Show();
            }
        }
    }
}
