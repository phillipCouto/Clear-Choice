using System;
using System.Windows.Controls;
using System.Windows.Input;

namespace Stemstudios.UIControls
{
    /// <summary>
    /// Interaction logic for IconButton.xaml
    /// </summary>
    public partial class IconButton : UserControl
    {
        public Image Source
        {
            get { return this.IconImage as Image; }
            set { this.IconImage.SetValue(Image.SourceProperty, value.GetValue(Image.SourceProperty)); }
        }
        public String Text
        {
            get { return this.ButtonText.GetValue(TextBlock.TextProperty) as String; }
            set { this.ButtonText.SetValue(TextBlock.TextProperty,value);}
        }
        public Boolean Horizontal
        {
            get { return this.ContentPanel.Orientation.Equals(Orientation.Horizontal); }
            set { this.ContentPanel.Orientation = (value ? Orientation.Horizontal : Orientation.Vertical); }
        }
        public IconButton()
        {
            InitializeComponent();
            
        }

        private void ContentPanel_MouseEnter(object sender, MouseEventArgs e)
        {
            Cursor = Cursors.Hand;
        }

        private void ContentPanel_MouseLeave(object sender, MouseEventArgs e)
        {
            Cursor = Cursors.Arrow;
        }

    }
}
