using System;
using System.Collections;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using ClearChoice.Views;
using Stemstudios.DataAccessLayer;
using Stemstudios.UIControls;

namespace ClearChoice
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        #region Variables used by the form.
        private Point lastClick; //Holds where the Form was clicked
        private Boolean isLeftExpanded = true;
        private Boolean CommonTasksExpanded = true;
        private Boolean isWindowed = true;
        internal static STabControl mainTabControl;
        internal static ActionList actionList;
        private Database db = Database.Instance;
        #endregion

        /// <summary>
        /// Initates the MainWindow.
        /// </summary>
        public MainWindow()
        {
            InitializeComponent();
            mainTabControl = new STabControl();
            mainTabControl.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#00000000"));
            mainTabControl.BorderThickness = new Thickness(0);
            TabControlContainer.Children.Add(mainTabControl);

            actionList = new ActionList();
            this.CommonTasksContent.Children.Add(actionList);

            Image tabIcon = (Image)App.iconSet["logo"];
            OpenTab(new HomeView(), tabIcon, "Home");
        }
        /// <summary>
        /// Static method used for adding new tabs to the Mainform tabcontroller.
        /// </summary>
        /// <param name="control"></param>
        /// <param name="tabIcon"></param>
        /// <param name="title"></param>
        public static void OpenTab(UserControl control, Image tabIcon, String title)
        {
            STabItem newTab = new STabItem(title, tabIcon);
            newTab.Name = "Tab" + control.Name;
            newTab.TabContent = control;
            if (!mainTabControl.TabAlreadyOpen(newTab))
            {
                mainTabControl.AddTab(newTab);
            }
            else
            {
                mainTabControl.SelectTab(newTab);
            }
        }
        /// <summary>
        /// Sets the Action List for the current Tab
        /// </summary>
        /// <param name="actions"></param>
        public static void setActionList(ArrayList actions)
        {
            actionList.Source = actions;
        }
        /// <summary>
        /// Updates a Tabs title and name.
        /// </summary>
        /// <param name="controlName"></param>
        /// <param name="newTitle"></param>
        /// <param name="newControlName"></param>
        public static void UpdateTabTitle(String controlName, String newTitle, String newControlName)
        {
            mainTabControl.ChangeTabTitle("Tab" + controlName, newTitle, "Tab" + newControlName);
        }
        /// <summary>
        /// Removes a tab with the provided control name.
        /// </summary>
        /// <param name="controlName"></param>
        public static void RemoveTab(String controlName)
        {
            mainTabControl.RemoveTab("Tab" + controlName);
        }
        /// <summary>
        /// Handles the movement of window.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TopStackPanel_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                    Cursor = Cursors.ScrollAll;
                    Point currentPos = e.GetPosition((IInputElement)sender);
                    this.Left += currentPos.X - lastClick.X;
                    this.Top += currentPos.Y - lastClick.Y;
            }
            else
            {
                Cursor = Cursors.Arrow;
            }
        }
        /// <summary>
        /// When the user wants to change the position this handles the movement.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TopStackPanel_MouseDown(object sender, MouseButtonEventArgs e)
        {
            lastClick = e.GetPosition((IInputElement)sender);
        }
        /// <summary>
        /// Minimizes the sidebar when the line is clicked.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Grid_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (isLeftExpanded)
            {
                BeginStoryboard((Storyboard)FindResource("SideBarAnimationShrink"));
                isLeftExpanded = false;
            }
            else
            {
                BeginStoryboard((Storyboard)FindResource("SideBarAnimationExpand"));
                isLeftExpanded = true;
            }
        }
        /// <summary>
        /// When the mouse enters the side bar minizmizer it changes to a hand.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Grid_MouseEnter(object sender, MouseEventArgs e)
        {
            Cursor = Cursors.Hand;
        }
        /// <summary>
        /// When the mouse leaves the side bar minimizer it changes back to arrow.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Grid_MouseLeave(object sender, MouseEventArgs e)
        {
            Cursor = Cursors.Arrow;
        }
        /// <summary>
        /// Changes the mouse to a Hand when it enters any of the left headers.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void UpComingExpanderBorder_MouseEnter(object sender, MouseEventArgs e)
        {
            Cursor = Cursors.Hand;
        }
        /// <summary>
        /// Changes the cursor back to an arrow and mouse leaves any of the left columns.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void UpComingExpanderBorder_MouseLeave(object sender, MouseEventArgs e)
        {
            Cursor = Cursors.Arrow;
        }
        /// <summary>
        /// Handles the event when the Common Tasks Header is clicked
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CommonTasksExpanderBorder_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (CommonTasksExpanded)
            {
                GridLengthAnimation gla = new GridLengthAnimation();
                gla.From = new GridLength(CommonTasksContentDef.ActualHeight);
                gla.To = new GridLength(0);
                gla.Duration = new Duration(new TimeSpan(1000000));
                CommonTasksContentDef.BeginAnimation(RowDefinition.HeightProperty, gla);
                CommonTasksExpanded = false;
                CommonTasksExpanderBorder.CornerRadius = new CornerRadius(3, 3, 3, 3);
                Double newSize = LeftContentGrid.ActualHeight;
                newSize = NewSizeForExpansion(newSize,0);
            }
            else
            {
                Double newSize = LeftContentGrid.ActualHeight;
                newSize = NewSizeForExpansion(newSize,1);
                GridLengthAnimation gla = new GridLengthAnimation();
                gla.From = new GridLength(0);
                gla.To = new GridLength(newSize, GridUnitType.Star);
                gla.Duration = new Duration(new TimeSpan(1000000));
                CommonTasksContentDef.BeginAnimation(RowDefinition.HeightProperty, gla);
                CommonTasksExpanded = true;
                CommonTasksExpanderBorder.CornerRadius = new CornerRadius(3, 3, 0, 0);
            }
        }
        /// <summary>
        /// Handles the event when the Close Icon is clicked.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CloseIcon_MouseDown(object sender, MouseButtonEventArgs e)
        {
            MessageBoxResult results = MessageBox.Show("You are about to exit the application. Any unsaved work will be lost. Are you sure you want to continue?", "Application Closing", MessageBoxButton.YesNo, MessageBoxImage.Exclamation);
            if (results == MessageBoxResult.Yes)
            {
                db.CloseConnection();
                Environment.Exit(0);
            }
        }
        /// <summary>
        /// When the maxwindow button is click this event is handled here.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MaxWindowIcon_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (isWindowed)
            {
                this.WindowState = WindowState.Maximized;
                isWindowed = false;
                MaxWindowIcon.Source = ((Image)Resources["windowIcon"]).Source;
            }
            else
            {
                this.WindowState = WindowState.Normal;
                isWindowed = true;
                MaxWindowIcon.Source = ((Image)Resources["maximizeIcon"]).Source;
            }
        }
        /// <summary>
        /// When a mouse enters the icon it changes the mouse to a hand cursor
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CloseIcon_MouseEnter(object sender, MouseEventArgs e)
        {
            Cursor = Cursors.Hand;
        }
        /// <summary>
        /// When a mouse leaves the icon it sets the mouse cursor back to an arrow.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CloseIcon_MouseLeave(object sender, MouseEventArgs e)
        {
            Cursor = Cursors.Arrow;
        }
        /// <summary>
        /// Handles the event when the Minimize icon is clicked.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MinimizeIcon_MouseDown(object sender, MouseButtonEventArgs e)
        {
            WindowState = WindowState.Minimized;
        }
        /// <summary>
        /// Calculates the new size for the Left element being expanded.
        /// </summary>
        /// <param name="totalSize"></param>
        /// <param name="startingPoint"></param>
        /// <returns></returns>
        private double NewSizeForExpansion(Double totalSize, Double startingPoint)
        {
            Double newSize = totalSize;
            if (CommonTasksExpanded)
            {
                GridLengthAnimation gla = new GridLengthAnimation();
                gla.From = new GridLength(CommonTasksContentDef.ActualHeight);
                gla.To = new GridLength(newSize, GridUnitType.Star);
                gla.Duration = new Duration(new TimeSpan(1000000));
                CommonTasksContentDef.BeginAnimation(RowDefinition.HeightProperty, gla);
            }
            return newSize;
        }
        /// <summary>
        /// Handles the closing of the window using Windows OS
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            MessageBoxResult results = MessageBox.Show("You are about to exit the application. Any unsaved work will be lost. Are you sure you want to continue?", "Application Closing", MessageBoxButton.YesNo, MessageBoxImage.Exclamation);
            if (results == MessageBoxResult.Yes)
            {
                db.CloseConnection();
                Environment.Exit(0);
            }
        }

    }
}
