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

namespace Clear_Choice.Windows
{
    /// <summary>
    /// Interaction logic for GeneratedPasswordDisplay.xaml
    /// </summary>
    public partial class GeneratedPasswordDisplay : Window
    {
        public GeneratedPasswordDisplay(String password,String email)
        {
            InitializeComponent();
            txtPassword.Text = password;
            txtEmail.Text = email;
        }

        private void cmdClose_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
