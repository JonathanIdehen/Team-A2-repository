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

namespace Budweg.View
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly EmployeeRepository employeeRepository; // repo for at hente medarbejderdata

        public MainWindow()
        {
            InitializeComponent(); 
            employeeRepository = new EmployeeRepository();
        }

        private void CheckIn_Click(object sender, RoutedEventArgs e) // event handler for Check Ind knappen
        {
            txtMessage.Text = "";

            if (!int.TryParse(txtEmployeeId.Text, out int employeeId)) // validering af input, tjekker om det er et tal
            {
                txtMessage.Text = "Medarbejder-ID skal være et tal."; // hvis det ikke er et tal, vises en fejlmeddelelse
                return;
            }

            Employee? employee = employeeRepository.GetEmployeeById(employeeId); // henter medarbejderdata baseret på det indtastede ID

            if (employee != null) // hvis medarbejderen findes, vises en succesmeddelelse
            {
                txtMessage.Text = "Tak, du er nu tjekket ind.";

                // Her kan I senere åbne næste vindue
                // Example:
                // MenuWindow menuWindow = new MenuWindow(employee);
                // menuWindow.Show();
                // this.Close();
                if (employee != null)
                {
                    MenuWindow menuWindow = new MenuWindow();
                    menuWindow.Show();
                    this.Close();
                }
            }
            else
            {
                txtMessage.Text = "Medarbejder-ID findes ikke."; // hvis medarbejderen ikke findes, vises en fejlmeddelelse
            }
        }


    }



}