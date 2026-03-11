using Budweg.Model;
using Budweg.Persistens;
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
using Budweg.View;
using Budweg.ViewModel;

namespace Budweg.View
{
    public partial class MainWindow : Window
    {
        private readonly EmployeeViewModel employeeViewModel;

        public MainWindow()
        {
            InitializeComponent();
            employeeViewModel = new EmployeeViewModel();
            DataContext = employeeViewModel;
        }

        private void CheckIn_Click(object sender, RoutedEventArgs e)
        {
            employeeViewModel.CheckIn();

            if (employeeViewModel.ActiveEmployee != null)
            {
                MenuWindow menuWindow = new MenuWindow(employeeViewModel.ActiveEmployee);
                menuWindow.Show();
                this.Close();
            }
        }
    }
}