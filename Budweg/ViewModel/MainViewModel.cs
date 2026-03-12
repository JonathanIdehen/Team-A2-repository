using Budweg.Model;
using Budweg.ViewModel;

namespace Budweg.ViewModel
{
    public class MainViewModel
    {
        public Employee ActiveEmployee { get; }

        public CaliperViewModel CaliperViewModel { get; }
        public HistoryViewModel HistoryViewModel { get; }
        public StartControlViewModel StartControlViewModel { get; }
        public FinalControlViewModel FinalControlViewModel { get; }

        public MainViewModel(Employee activeEmployee)
        {
            ActiveEmployee = activeEmployee;

            CaliperViewModel = new CaliperViewModel();
            HistoryViewModel = new HistoryViewModel();
            StartControlViewModel = new StartControlViewModel(activeEmployee.EmployeeID);
            FinalControlViewModel = new FinalControlViewModel(activeEmployee.EmployeeID);

            HistoryViewModel.LoadLatestHistory();
        }
    }
}
