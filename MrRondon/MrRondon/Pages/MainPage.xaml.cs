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
                    case 2:
                        {
                            var service = new EventService();
                            var items = await service.GetAsync();
                            var pageModel = new ListEventPageModel();
                            pageModel.Items.ReplaceRange(items);
                            pageModel.LoadCitiesCommand.Execute(null);
                            CurrentPage.BindingContext = pageModel;
                            return;
                        }
                    case 3:
                        {
                            var service = new HistoricalSightService();
                            var items = await service.GetAsync();
                            var pageModel = new ListHistoricalSightPageModel();
                            pageModel.Items.ReplaceRange(items);
                            pageModel.LoadCitiesCommand.Execute(null);
                            CurrentPage.BindingContext = pageModel;
                            return;
                        }
                    default:
                        return;
                }
            };
        }

    }
}