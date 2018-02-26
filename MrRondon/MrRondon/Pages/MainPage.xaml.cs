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
                            //var service = new CategoryService();
                            //var items = await service.GetAsync();
                            //CurrentPage.BindingContext = new ListCategoriesPageModel
                            //{
                            //    Items = new ObservableRangeCollection<ViewModels.CategoryListVm>(items)
                            //};

                            var pageModel = new ListCategoriesPageModel();
                            pageModel.LoadItemsCommand.Execute(null);
                            CurrentPage.BindingContext = pageModel;
                            return;
                        }
                    case 2:
                        {
                            var service = new EventService();
                            var pageModel = new ListEventPageModel();
                            pageModel.LoadCitiesCommand.Execute(null);
                            var items = await service.GetAsync();
                            pageModel.Items.ReplaceRange(items);
                            CurrentPage.BindingContext = pageModel;
                            return;
                        }
                    case 3:
                        {
                            var service = new HistoricalSightService();
                            var pageModel = new ListHistoricalSightPageModel();
                            pageModel.LoadCitiesCommand.Execute(null);
                            var items = await service.GetAsync();
                            pageModel.Items.ReplaceRange(items);
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