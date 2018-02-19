using MrRondon.Helpers;
using MrRondon.Pages.Category;
using MrRondon.Pages.Event;
using MrRondon.Pages.HistoricalSight;
using MrRondon.Services;
using Xamarin.Forms;

namespace MrRondon.Pages
{
    public partial class MainPage : TabbedPage
    {
        public MainPage()
        {
            InitializeComponent();
            BindingContext = new MainPageModel();
            CurrentPageChanged += async (sender, e) =>
            {
                var numPage = Children.IndexOf(CurrentPage);

                switch (numPage)
                {
                    case 1:
                        {
                            var service = new CategoryService();
                            var items = await service.GetAsync();
                            CurrentPage.BindingContext = new ListCategoriesPageModel
                            {
                                Items = new ObservableRangeCollection<Entities.SubCategory>(items)
                            };
                            return;
                        }
                    default:
                        return;
                }
            };
        }

    }
}