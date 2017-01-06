using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using DatamodelBuild.Exceptions;

namespace DatamodelBuildClient
{
    /// <summary>
    /// Interaction logic for ErrorWindow.xaml
    /// </summary>
    public partial class ErrorWindow : Window
    {
        public ErrorWindow()
        {
            InitializeComponent();
        }

        public void setMessage(CustomCOMException e)
        {
            lblError.Content = e.ToString();
        }

        void OnClick_btnOK(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
