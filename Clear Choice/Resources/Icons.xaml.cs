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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Clear_Choice.Resources
{
    /// <summary>
    /// Interaction logic for Icons.xaml
    /// </summary>
    public partial class Icons : UserControl
    {
        public Icons()
        {
            InitializeComponent();
        }
        public Image getSource(String name)
        {
            return ((Image)this.Resources[name]);
        }
    }
}
