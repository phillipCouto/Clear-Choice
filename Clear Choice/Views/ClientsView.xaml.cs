using System;
using System.Collections;
using System.Resources;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Threading;
using ExceptionLogging;
using Stemstudios.DataAccessLayer;
using Stemstudios.DataAccessLayer.DataObjects;
using Stemstudios.UIControls;

namespace ClearChoice.Views
{
    /// <summary>
    /// Interaction logic for ClientsView.xaml
    /// </summary>
    public partial class ClientsView : UserControl
    {
        private Hashtable clientsTable = new Hashtable();
        private Database db = Database.Instance;
        private ResourceManager msgCodes = MessageCodes.ResourceManager;

        public ClientsView()
        {
            InitializeComponent();
            this.Name = "ClientsViewList";
            loadClientDataSet();
        }

        private void loadClientDataSet()
        {
            Thread clientsLoadingThread = new Thread(LoadDataSet);
            clientsLoadingThread.SetApartmentState(ApartmentState.STA);
            clientsLoadingThread.Start();
        }
        private void TextBlock_MouseEnter(object sender, MouseEventArgs e)
        {
            Cursor = Cursors.Hand;
            DoubleAnimation scaleText = new DoubleAnimation();
            scaleText.From = ((TextBlock)sender).FontSize;
            scaleText.To = 22;
            scaleText.Duration = new TimeSpan(1500000);
            ((TextBlock)sender).BeginAnimation(TextBlock.FontSizeProperty, scaleText);
            int pos = this.Clients.Children.IndexOf((TextBlock)sender);
            if (pos > 0)
            {
                scaleText = new DoubleAnimation();
                scaleText.From = ((TextBlock)this.Clients.Children[pos - 1]).FontSize;
                scaleText.To = 18;
                scaleText.Duration = new TimeSpan(1500000);
                ((TextBlock)this.Clients.Children[pos - 1]).BeginAnimation(TextBlock.FontSizeProperty, scaleText);
            }
            if (pos < this.Clients.Children.Count - 1)
            {
                scaleText = new DoubleAnimation();
                scaleText.From = ((TextBlock)this.Clients.Children[pos + 1]).FontSize;
                scaleText.To = 18;
                scaleText.Duration = new TimeSpan(1500000);
                ((TextBlock)this.Clients.Children[pos + 1]).BeginAnimation(TextBlock.FontSizeProperty, scaleText);
            }
            if (pos > 1)
            {
                scaleText = new DoubleAnimation();
                scaleText.From = ((TextBlock)this.Clients.Children[pos - 2]).FontSize;
                scaleText.To = 14;
                scaleText.Duration = new TimeSpan(1500000);
                ((TextBlock)this.Clients.Children[pos - 2]).BeginAnimation(TextBlock.FontSizeProperty, scaleText);
            }
            if (pos < this.Clients.Children.Count - 2)
            {
                scaleText = new DoubleAnimation();
                scaleText.From = ((TextBlock)this.Clients.Children[pos + 2]).FontSize;
                scaleText.To = 14;
                scaleText.Duration = new TimeSpan(1500000);
                ((TextBlock)this.Clients.Children[pos + 2]).BeginAnimation(TextBlock.FontSizeProperty, scaleText);
            }
        }
        
        private void TextBlock_MouseLeave(object sender, MouseEventArgs e)
        {
            Cursor = Cursors.Arrow;
        }

        private void TextBlock_DoubleClick(object sender, MouseEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                Client selectedClick = (Client)clientsTable[((TextBlock)sender).Name];
                Image icon = (Image)App.iconSet["customer1"];
                MainWindow.OpenTab(new ClientView(selectedClick), icon, selectedClick.GetName());
                MainWindow.RemoveTab(this.Name);
            }
        }

        private void UserControl_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if (IsVisible)
            {
                ArrayList actions = new ArrayList();
                IconButton newClientButton = new IconButton();
                newClientButton.Text = "Add New Client";
                newClientButton.Source = (Image)App.iconSet["customer1"];
                newClientButton.MouseDown += new MouseButtonEventHandler(newClientButton_MouseDown);
                actions.Add(newClientButton);
                MainWindow.setActionList(actions);
                if (IsLoaded)
                {
                    loadClientDataSet();
                }
            }
        }

        private void newClientButton_MouseDown(object sender, MouseButtonEventArgs e)
        {
            MainWindow.OpenTab(new ClientView(), (Image)App.iconSet["customer1"], "New Client");
            MainWindow.RemoveTab(this.Name);
        }

        private void LoadDataSet()
        {
            try
            {
                DataSet data = db.Select("*", Client.Table,null,Client.Fields.ClientType.ToString()+","+Client.Fields.Name.ToString());
                DispatcherOperation op = Dispatcher.BeginInvoke(DispatcherPriority.Normal, new Action<DataSet>(UILoadDataSet), data);

                DispatcherOperationStatus status = op.Status;
                while (status != DispatcherOperationStatus.Completed)
                {
                    status = op.Wait(TimeSpan.FromMilliseconds(1000));
                    if (status == DispatcherOperationStatus.Aborted)
                    {
                        Console.WriteLine("Failed");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Loading Clients - " + msgCodes.GetString("M2102") + ex.Message, "Error - 2102", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        
        private void UILoadDataSet(DataSet clientsData)
        {
            this.Clients.Children.Clear();
            if (clientsData.NumberOfRows() != 0)
            {
                bool doneBuilders = false;
                while (clientsData.Read())
                {
                    Client client = new Client(clientsData.GetRecordDataSet());
                    TextBlock block = new TextBlock();
                    block.Text = client.GetName();
                    block.Name = "Client" + client.GetClientID();
                    clientsTable.Add(block.Name, client);
                    Color forground = new Color();
                    forground.ScA = 1;
                    forground.ScR = 1;
                    forground.ScG = 1;
                    forground.ScB = 1;
                    block.Foreground = new SolidColorBrush(forground);
                    block.FontSize = 14;
                    block.MouseEnter += TextBlock_MouseEnter;
                    block.MouseLeave += TextBlock_MouseLeave;
                    block.MouseDown += TextBlock_DoubleClick;
                    if (!doneBuilders & client.GetClientType() == 1)
                    {
                        Thickness padding = new Thickness();
                        padding.Top = 15;
                        block.Padding = padding;
                        doneBuilders = true;
                    }
                    this.Clients.Children.Add(block);
                }
            }
            else
            {
                MessageBox.Show("There are currently no clients in the System. Please create a client.", "No Clients Exist!", MessageBoxButton.OK, MessageBoxImage.Error);
                MainWindow.RemoveTab(this.Name);
            }
        }
    }
}
