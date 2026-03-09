using Budweg.Model;
using Budweg.Persistens;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Budweg.ViewModel
{
    public class HistoryViewModel : INotifyPropertyChanged
    {
        private readonly CaliperRepository caliperRepository = new();

        private string historyCaliperIdText = "";
        private string historyMessage = "";

        public string HistoryCaliperIdText
        {
            get => historyCaliperIdText;
            set
            {
                historyCaliperIdText = value;
                OnPropertyChanged();
            }
        }

        public string HistoryMessage
        {
            get => historyMessage;
            set
            {
                historyMessage = value;
                OnPropertyChanged();
            }
        }

        public ObservableCollection<Caliper> HistoryResults { get; set; } = new();

        public void LoadLatestHistory()
        {
            HistoryMessage = "";
            HistoryResults.Clear();

            List<Caliper> latestCalipers = caliperRepository.GetLatestCalipers();

            foreach (Caliper caliper in latestCalipers)
            {
                HistoryResults.Add(caliper);
            }

            if (HistoryResults.Count == 0)
            {
                HistoryMessage = "Der findes ingen bremsekalibre endnu.";
            }
        }

        public void SearchHistory()
        {
            HistoryMessage = "";
            HistoryResults.Clear();

            if (!int.TryParse(HistoryCaliperIdText, out int caliperId))
            {
                HistoryMessage = "BremsekaliberID skal være et tal.";
                return;
            }

            Caliper? caliper = caliperRepository.GetCaliperById(caliperId);

            if (caliper == null)
            {
                HistoryMessage = "Der blev ikke fundet en bremsekaliber med det ID.";
                return;
            }

            HistoryResults.Add(caliper);
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        private void OnPropertyChanged([CallerMemberName] string? propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
