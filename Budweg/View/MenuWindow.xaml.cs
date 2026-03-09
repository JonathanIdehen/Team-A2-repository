using Budweg.Model;
using Budweg.Persistens;
using Budweg.ViewModel;
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
        private readonly CaliperViewModel caliperViewModel;
        private readonly HistoryViewModel historyViewModel;

        public CaliperViewModel CaliperViewModel => caliperViewModel;
        public HistoryViewModel HistoryViewModel => historyViewModel;

        public MenuWindow()
        {
            InitializeComponent();
            caliperViewModel = new CaliperViewModel();
            historyViewModel = new HistoryViewModel();
            DataContext = this;
            historyViewModel.LoadLatestHistory();

        }

        private void ShowCaliperType_Click(object sender, RoutedEventArgs e)
        {
            caliperViewModel.ShowCaliperType();
        }

        private void SaveCaliper_Click(object sender, RoutedEventArgs e)
        {
            caliperViewModel.SaveCaliper();
        }

        private void SearchHistory_Click(object sender, RoutedEventArgs e)
        {
            historyViewModel.SearchHistory();
        }

        private void ShowLatestHistory_Click(object sender, RoutedEventArgs e)
        {
            historyViewModel.LoadLatestHistory();
        }
    }
}
