using Budweg.Model;
using Budweg.Persistens;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;
using System.Windows.Input;

namespace Budweg.ViewModel
{
    public class FinalControlViewModel : INotifyPropertyChanged
    {
        private readonly FinalControlRepository finalControlRepository = new();
        private readonly int employeeID;

        public ICommand SaveFinalControlCommand { get; }

        private string caliperIDText = "";
        private DateTime? controlDate = DateTime.Now;
        private bool result;
        private bool waste;
        private bool export;
        private string comment = "";
        private string message = "";

        

        public int EmployeeID => employeeID;

        public string CaliperIDText
        {
            get => caliperIDText;
            set { caliperIDText = value; OnPropertyChanged(); }
        }

        public DateTime? ControlDate
        {
            get => controlDate;
            set { controlDate = value; OnPropertyChanged(); }
        }

        public bool Result
        {
            get => result;
            set { result = value; OnPropertyChanged(); }
        }

        public bool Waste
        {
            get => waste;
            set { waste = value; OnPropertyChanged(); }
        }

        public bool Export
        {
            get => export;
            set { export = value; OnPropertyChanged(); }
        }

        public string Comment
        {
            get => comment;
            set { comment = value; OnPropertyChanged(); }
        }

        public string Message
        {
            get => message;
            set { message = value; OnPropertyChanged(); }
        }

        public FinalControlViewModel(int employeeID)
        {
            this.employeeID = employeeID;
            SaveFinalControlCommand = new RelayCommand(SaveFinalControl);
        }

        public void SaveFinalControl()
        {
            Message = "";

            if (!int.TryParse(CaliperIDText, out int caliperId))
            {
                Message = "BremsekaliberID skal være et tal.";
                return;
            }

            if (ControlDate == null)
            {
                Message = "Du skal vælge en dato.";
                return;
            }

            FinalControl finalControl = new()
            {
                EmployeeID = employeeID,
                CaliperID = caliperId,
                Date = ControlDate.Value,
                Result = Result,
                Waste = Waste,
                Export = Export,
                Comment = Comment
            };

            finalControlRepository.AddFinalControl(finalControl);
            Message = "Slutkontrollen er gemt.";
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        private void OnPropertyChanged([CallerMemberName] string? name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }
}
