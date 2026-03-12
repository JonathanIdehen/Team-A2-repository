using System;
using System.Collections.Generic;
using System.Text;
using Budweg.Model;
using Budweg.Persistens;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Budweg.ViewModel
{
    public class StartControlViewModel : INotifyPropertyChanged
    {
        private readonly StartControlRepository startControlRepository = new();
        private readonly int employeeID;

        private string caliperIDText = "";
        private DateTime? controlDate = DateTime.Now;
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

        public string Message
        {
            get => message;
            set { message = value; OnPropertyChanged(); }
        }

        public StartControlViewModel(int employeeID)
        {
            this.employeeID = employeeID;
        }

        public void SaveStartControl()
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

            StartControl startControl = new()
            {
                EmployeeID = employeeID,
                CaliperID = caliperId,
                Date = ControlDate.Value
            };

            startControlRepository.AddStartControl(startControl);
            Message = "Indgangskontrollen er gemt.";
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        private void OnPropertyChanged([CallerMemberName] string? name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }
}
