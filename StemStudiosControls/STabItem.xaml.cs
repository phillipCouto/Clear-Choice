using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;

namespace Stemstudios.UIControls
{
    /// <summary>
    /// Interaction logic for IconButton.xaml
    /// </summary>
    public partial class STabItem : UserControl
    {
        public event EventHandler<MouseButtonEventArgs> IconClicked;
        public event EventHandler<EventArgs> IconClosing;

        private Boolean IsTabSelected = false;
        private UserControl TabContentControl;
        private String tabTitle;
        public Boolean IsSelected
        {
            get { return this.IsTabSelected; }
            set { this.IsTabSelected = value; SelectionChanged(); }
        }
        public UserControl TabContent
        {
            get { return this.TabContentControl; }
            set { this.TabContentControl = value; }
        }
        public String TabTitle
        {
            get { return this.tabTitle; }
            set { this.tabTitle = value; ButtonText.Text = tabTitle; }
        }
        
        public STabItem()
        {
            InitializeComponent();
            this.ButtonText.Text = "IconButton";
            Color NormalColor = new Color();
            NormalColor.ScA = 1;
            NormalColor.ScB = 0;
            NormalColor.ScG = 0;
            NormalColor.ScR = 0;
            this.ButtonText.Foreground = new SolidColorBrush(NormalColor);
        }
        
        public STabItem(String Text, Image pIconImage)
        {
            InitializeComponent();
            TabTitle = Text;
            IconImageHolder.Source = pIconImage.Source;
        }
        
        private void SelectionChanged()
        {
            if (IsSelected)
            {
                Storyboard anim = (Storyboard)this.Resources["SelectedAnim"];
                anim.Begin();
            }
            else
            {
                Storyboard anim = (Storyboard)this.Resources["DeSelectedAnim"];
                anim.Begin();
            }
        }

        private void Border_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                e.Handled = true;
                if (IconClicked != null)
                    IconClicked(this, e);
            }
            else if (e.RightButton == MouseButtonState.Pressed)
            {
                ContextMenu newMenu = new ContextMenu();
                MenuItem text = new MenuItem();
                text.Header = "Close Tab";
                text.Click += CloseText_MouseDown;
                newMenu.Items.Add(text);
                newMenu.PlacementTarget = this;
                newMenu.IsOpen = true;
            }
        }

        private void Border_MouseEnter(object sender, MouseEventArgs e)
        {
            if (!IsSelected)
            {
                Storyboard anim = (Storyboard)this.Resources["MouseEnterAnim"];
                anim.Begin();
            }
        }

        private void Border_MouseLeave(object sender, MouseEventArgs e)
        {
            if (!IsSelected)
            {
                Storyboard anim = (Storyboard)this.Resources["MouseExitAnim"];
                anim.Begin();
            }

        }

        private void CloseText_MouseDown(object sender, RoutedEventArgs e)
        {
            if (IconClosing != null)
            {
                IconClosing(this, e);
            }
                e.Handled = true;
        }

    }
}
