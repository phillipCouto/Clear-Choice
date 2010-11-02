using System.Collections;
using System.Windows;
using System.Windows.Controls;
using Stemstudios.UIControls;

namespace Stemstudios.UIControls
{
    /// <summary>
    /// Interaction logic for ActionList.xaml
    /// </summary>
    public partial class ActionList : UserControl
    {
        public ArrayList Source
        {
            set { parseCollection(value); }
        }
        public ActionList()
        {
            InitializeComponent();
        }

        public void parseCollection(ArrayList buttons)
        {
            ActionListGrid.Children.Clear();
            foreach (IconButton button in buttons)
            {
                button.Horizontal = true;
                button.HorizontalAlignment = HorizontalAlignment.Left;
                button.VerticalAlignment = VerticalAlignment.Top;
                this.ActionListGrid.Children.Add(button);
            }
        }
    }
}
