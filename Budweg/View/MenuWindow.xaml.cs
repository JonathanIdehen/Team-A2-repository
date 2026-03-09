using Budweg.Model;
using Budweg.Persistens;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Budweg.View
{
    /// <summary>
    /// Interaction logic for MenuWindow.xaml
    /// </summary>
    public partial class MenuWindow : Window
    {
        private readonly CaliperRepository caliperRepository;

        public MenuWindow()
        {
            InitializeComponent();
            caliperRepository = new CaliperRepository();

            LoadLatestHistory();
        }

        private void SearchHistory_Click(object sender, RoutedEventArgs e)
        {
            txtHistoryMessage.Text = "";
            dgHistory.ItemsSource = null;

            if (!int.TryParse(txtHistoryCaliperId.Text, out int caliperId))
            {
                txtHistoryMessage.Text = "BremsekaliberID skal være et tal.";
                return;
            }

            Caliper? caliper = caliperRepository.GetCaliperById(caliperId);

            if (caliper == null)
            {
                txtHistoryMessage.Text = "Der blev ikke fundet en bremsekaliber med det ID.";
                return;
            }

            dgHistory.ItemsSource = new List<Caliper> { caliper };
        }

        private void ShowLatestHistory_Click(object sender, RoutedEventArgs e)
        {
            txtHistoryCaliperId.Clear();
            LoadLatestHistory();
        }

        private void LoadLatestHistory()
        {
            txtHistoryMessage.Text = "";
            dgHistory.ItemsSource = null;

            List<Caliper> latestCalipers = caliperRepository.GetLatestCalipers();

            dgHistory.ItemsSource = latestCalipers;

            if (latestCalipers.Count == 0)
            {
                txtHistoryMessage.Text = "Der findes ingen bremsekalibre endnu.";
            }
        }

        private void ShowCaliperType_Click(object sender, RoutedEventArgs e)
        {
            txtCaliperMessage.Text = "";
            txtCaliperTypeResult.Text = "";

            if (!int.TryParse(txtCaliperId.Text, out int caliperId))
            {
                txtCaliperMessage.Text = "BremsekaliberID skal være et tal.";
                return;
            }

            if (!int.TryParse(txtItemNumber.Text, out int itemNumber))
            {
                txtCaliperMessage.Text = "Varenummer skal være et tal.";
                return;
            }

            Caliper caliper = new Caliper
            {
                CaliperID = caliperId,
                ItemNumber = itemNumber
            };

            caliper.UpdateCaliperType();
            txtCaliperTypeResult.Text = caliper.CaliperType;
        }

        private void SaveCaliper_Click(object sender, RoutedEventArgs e)
        {
            txtCaliperMessage.Text = "";
            txtCaliperTypeResult.Text = "";

            if (!int.TryParse(txtCaliperId.Text, out int caliperId))
            {
                txtCaliperMessage.Text = "BremsekaliberID skal være et tal.";
                return;
            }

            if (!int.TryParse(txtItemNumber.Text, out int itemNumber))
            {
                txtCaliperMessage.Text = "Varenummer skal være et tal.";
                return;
            }

            Caliper caliper = new Caliper
            {
                CaliperID = caliperId,
                ItemNumber = itemNumber
            };

            caliper.UpdateCaliperType();

            if (caliper.CaliperType == "Ukendt type")
            {
                txtCaliperMessage.Text = "Varenummeret giver ikke en gyldig bremsekalibertype.";
                return;
            }

            caliperRepository.AddCaliper(caliper);
            txtCaliperTypeResult.Text = caliper.CaliperType;
            txtCaliperMessage.Text = "Bremsekaliberen er gemt.";
        }
    }
}
