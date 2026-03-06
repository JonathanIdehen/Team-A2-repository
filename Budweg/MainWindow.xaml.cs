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

namespace Budweg
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            FinalControlRepository repo = new FinalControlRepository();

            FinalControl finalControl = new FinalControl
            {
                Date = DateTime.Now,
                Result = true,
                Comment = "Godkendt",
                Waste = false,
                Export = true,
                CaliperID = 1,
                EmployeeID = 1
            };

            repo.AddFinalControl(finalControl);
        }


    }



}