using Budweg.Model;
using Budweg.Persistens;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Budweg.ViewModel
{
    public class CaliperViewModel : INotifyPropertyChanged
    {
        private readonly CaliperRepository caliperRepository = new();

        private string caliperIDText = "";
        private string itemNumberText = "";
        private string caliperTypeResult = "";
        private string caliperMessage = "";

        public string CaliperIDText
        {
            get => caliperIDText;
            set { caliperIDText = value; OnPropertyChanged(); }
        }

        public string ItemNumberText
        {
            get => itemNumberText;
            set { itemNumberText = value; OnPropertyChanged(); }
        }

        public string CaliperTypeResult
        {
            get => caliperTypeResult;
            set { caliperTypeResult = value; OnPropertyChanged(); }
        }

        public string CaliperMessage
        {
            get => caliperMessage;
            set { caliperMessage = value; OnPropertyChanged(); }
        }

        public void ShowCaliperType()
        {
            CaliperMessage = "";
            CaliperTypeResult = "";

            if (!int.TryParse(CaliperIDText, out int caliperId))
            {
                CaliperMessage = "BremsekaliberID skal være et tal.";
                return;
            }

            if (!int.TryParse(ItemNumberText, out int itemNumber))
            {
                CaliperMessage = "Varenummer skal være et tal.";
                return;
            }

            Caliper caliper = new()
            {
                CaliperID = caliperId,
                ItemNumber = itemNumber
            };

            caliper.UpdateCaliperType();
            CaliperTypeResult = caliper.CaliperType ?? "";
        }

        public void SaveCaliper()
        {
            if (!int.TryParse(CaliperIDText, out int caliperId) ||
                !int.TryParse(ItemNumberText, out int itemNumber))
            {
                CaliperMessage = "Ugyldigt input.";
                return;
            }

            Caliper caliper = new()
            {
                CaliperID = caliperId,
                ItemNumber = itemNumber
            };

            caliper.UpdateCaliperType();
            caliperRepository.AddCaliper(caliper);

            CaliperTypeResult = caliper.CaliperType ?? "";
            CaliperMessage = "Bremsekaliberen er gemt.";
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        private void OnPropertyChanged([CallerMemberName] string? name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }
}