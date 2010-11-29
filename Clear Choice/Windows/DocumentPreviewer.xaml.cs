using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Printing;
using System.Collections;

namespace Clear_Choice.Windows
{
    /// <summary>
    /// Interaction logic for DocumentPreviewer.xaml
    /// </summary>
    public partial class DocumentPreviewer : Window
    {
        private FlowDocument mDoc;
        private String title;
        public DocumentPreviewer(FlowDocument doc,String docTitle)
        {
            InitializeComponent();
            docViewer.Document = doc;
            mDoc = doc;
            title = docTitle;
            SetDocumentSize();
        }

        private void cmdPrint_Click(object sender, RoutedEventArgs e)
        {
            PrintDialog pd = new PrintDialog();
            if (pd.ShowDialog().GetValueOrDefault())
            {
                PrintTicket ticket = pd.PrintTicket;

                if (cmboOrientation.SelectedIndex == 0)
                {
                    ticket.PageOrientation = PageOrientation.Landscape;
                }
                else
                {
                    ticket.PageOrientation = PageOrientation.Portrait;
                }
                pd.PrintTicket = ticket;


                mDoc.PageHeight = pd.PrintableAreaHeight;
                mDoc.PageWidth = pd.PrintableAreaWidth;
                mDoc.PagePadding = new Thickness(50);
                mDoc.ColumnGap = 0;
                mDoc.ColumnWidth = pd.PrintableAreaWidth;

                IDocumentPaginatorSource dps = mDoc;
                pd.PrintDocument(dps.DocumentPaginator, title);
            }
        }

        private void SetDocumentSize()
        {
            PrintQueue printQueue = null;

            LocalPrintServer localPrintServer = new LocalPrintServer();

            // Retrieving collection of local printer on user machine
            PrintQueueCollection localPrinterCollection =
                localPrintServer.GetPrintQueues();

            System.Collections.IEnumerator localPrinterEnumerator =
                localPrinterCollection.GetEnumerator();

            if (localPrinterEnumerator.MoveNext())
            {
                // Get PrintQueue from first available printer
                printQueue = (PrintQueue)localPrinterEnumerator.Current;
                PrintTicket ticket = printQueue.DefaultPrintTicket;
                if (cmboOrientation.SelectedIndex == 0)
                {
                    ticket.PageOrientation = PageOrientation.Landscape;
                }
                else
                {
                    ticket.PageOrientation = PageOrientation.Portrait;
                }
                System.Printing.PrintCapabilities capabilities = printQueue.GetPrintCapabilities(ticket);
                double scale = Math.Min(capabilities.PageImageableArea.ExtentWidth / this.ActualWidth, capabilities.PageImageableArea.ExtentHeight /
                    this.ActualHeight);

                Size sz = new Size(capabilities.PageImageableArea.ExtentWidth, capabilities.PageImageableArea.ExtentHeight);

                mDoc.PageHeight = sz.Height;
                mDoc.PageWidth = sz.Width;
                mDoc.PagePadding = new Thickness(50);
                mDoc.ColumnGap = 0;
                mDoc.ColumnWidth = sz.Width;

            }
        }

        private void cmboOrientation_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (this.IsLoaded)
            {
                SetDocumentSize();
            }
        }
    }
}
