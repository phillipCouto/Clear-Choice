using System;
using System.Windows.Controls;
using System.Windows.Media;

namespace Stemstudios.UIControls
{
    /// <summary>
    /// Interaction logic for CategoryExpander.xaml
    /// </summary>
    public partial class CategoryExpander : UserControl
    {
        public ImageSource Source
        {
            get { return this.IconImage.GetValue(Image.SourceProperty) as ImageSource; }
            set { this.IconImage.SetValue(Image.SourceProperty, value); }
        }
        public String Text
        {
            get { return this.CategoryTitle.GetValue(TextBlock.TextProperty) as String; }
            set { this.CategoryTitle.SetValue(TextBlock.TextProperty, value); }
        }
        public UIElementCollection Children
        {
            get { return this.ContentGrid.Children as UIElementCollection; }
        }
        public CategoryExpander()
        {
            InitializeComponent();
            
        }
    }
}
