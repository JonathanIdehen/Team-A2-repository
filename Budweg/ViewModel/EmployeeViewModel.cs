using System;
using System.Collections.Generic;
using System.Text;
using Budweg.Model;
using Budweg.Persistens;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Budweg.ViewModel
{
    public class EmployeeViewModel : INotifyPropertyChanged
    {
        private readonly EmployeeRepository employeeRepository = new();

        private string employeeIdText = "";
        private string message = "";
        private Employee? activeEmployee;

        public string EmployeeIdText
        {
            get => employeeIdText;
            set
            {
                employeeIdText = value;
                OnPropertyChanged();
            }
        }

        public string Message
        {
            get => message;
            set
            {
                message = value;
                OnPropertyChanged();
            }
        }

        public Employee? ActiveEmployee
        {
            get => activeEmployee;
            set
            {
                activeEmployee = value;
                OnPropertyChanged();
            }
        }

        public void CheckIn()
        {
            Message = "";
            ActiveEmployee = null;

            if (!int.TryParse(EmployeeIdText, out int employeeId))
            {
                Message = "Medarbejder-ID skal være et tal.";
                return;
            }

            Employee? employee = employeeRepository.GetEmployeeById(employeeId);

            if (employee != null)
            {
                ActiveEmployee = employee;
                Message = "Tak, du er nu tjekket ind.";
            }
            else
            {
                Message = "Medarbejder-ID findes ikke.";
            }
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        private void OnPropertyChanged([CallerMemberName] string? name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }
}
