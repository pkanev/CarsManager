using System.Printing;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Xps;

namespace Client.Wpf.Views.Print
{
    public partial class PrintWindow : Window
    {
        private FixedDocumentSequence document;

        public PrintWindow()
        {
            InitializeComponent();
        }

        public PrintWindow(FixedDocumentSequence document)
        {
            this.document = document;
            InitializeComponent();
            Preview.Document = document;
        }

        private void ButtonPrint_Click(object sender, RoutedEventArgs e)
            => PrintDocument();

        private void PrintDocument()
        {
            PrintDialog printDialog = new PrintDialog();
            printDialog.PrintQueue = LocalPrintServer.GetDefaultPrintQueue();
            printDialog.PrintTicket = printDialog.PrintQueue.DefaultPrintTicket;

            printDialog.PrintTicket.PageOrientation = PageOrientation.Landscape;
            printDialog.PrintTicket.PageScalingFactor = 90;
            printDialog.PrintTicket.PageMediaSize = new PageMediaSize(PageMediaSizeName.ISOA4);
            printDialog.PrintTicket.PageBorderless = PageBorderless.None;

            if (printDialog.ShowDialog().GetValueOrDefault())
            {
                document.PrintTicket = printDialog.PrintTicket;

                XpsDocumentWriter writer = PrintQueue.CreateXpsDocumentWriter(printDialog.PrintQueue);
                writer.WriteAsync(document, printDialog.PrintTicket);
            }

        }
    }
}
