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
    public partial class MenuWindow : Window
    {
        private readonly MainViewModel mainViewModel;

        public MenuWindow(Employee activeEmployee)
        {
            InitializeComponent();
            mainViewModel = new MainViewModel(activeEmployee);
            DataContext = mainViewModel;
        }

        private void ShowCaliperType_Click(object sender, RoutedEventArgs e)
        {
            mainViewModel.CaliperViewModel.ShowCaliperType();
        }

        private void SaveCaliper_Click(object sender, RoutedEventArgs e)
        {
            mainViewModel.CaliperViewModel.SaveCaliper();
        }

        private void SearchHistory_Click(object sender, RoutedEventArgs e)
        {
            mainViewModel.HistoryViewModel.SearchHistory();
        }

        private void ShowLatestHistory_Click(object sender, RoutedEventArgs e)
        {
            mainViewModel.HistoryViewModel.LoadLatestHistory();
        }

        private void SaveStartControl_Click(object sender, RoutedEventArgs e)
        {
            mainViewModel.StartControlViewModel.SaveStartControl();
        }

        private void SaveFinalControl_Click(object sender, RoutedEventArgs e)
        {
            mainViewModel.FinalControlViewModel.SaveFinalControl();
        }
    }
}