using System.Windows.Input;
using MrRondon.Helpers;

namespace MrRondon.Pages.HistoricalSight
{
    public class ListHistoricalSightPageModel : BasePageModel
    {
        private bool _notHhasItems;
        public bool NotHasItems
        {
            get => _notHhasItems;
            set => SetProperty(ref _notHhasItems, value);
        }

        private string _errorMessage;
        public string ErrorMessage
        {
            get => _errorMessage;
            set => SetProperty(ref _errorMessage, value);
        }

        public ICommand LoadItemsCommand { get; set; }
        public ObservableRangeCollection<Entities.HistoricalSight> Items { get; set; }
    }
}