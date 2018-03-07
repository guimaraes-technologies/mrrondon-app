using System.Threading.Tasks;
using MrRondon.Pages.Category;
using MrRondon.Pages.Event;
using MrRondon.Pages.HistoricalSight;
using Xamarin.Forms;

namespace MrRondon.Pages
{
    public partial class MainPage : TabbedPage
    {
        public MainPage()
        {
            InitializeComponent();
            
            CurrentPageChanged += async (sender, e) =>
            {
                var numPage = Children.IndexOf(CurrentPage);

                switch (numPage)
                {
                    case 1:
                        {
                            var pageModel = new ListCategoriesPageModel();
                            pageModel.LoadItemsCommand.Execute(null);

                            CurrentPage.BindingContext = pageModel;
                            await Task.Delay(1);
                            return;
                        }
                    case 2:
                        {
                            var pageModel = new ListEventPageModel();
                            pageModel.LoadCitiesCommand.Execute(null);
                            pageModel.LoadItemsCommand.Execute(null);

                            CurrentPage.BindingContext = pageModel;
                            await Task.Delay(1);
                            return;
                        }
                    case 3:
                        {
                            var pageModel = new ListHistoricalSightPageModel();
                            pageModel.LoadCitiesCommand.Execute(null);
                            pageModel.LoadItemsCommand.Execute(null);

                            CurrentPage.BindingContext = pageModel;
                            await Task.Delay(1);
                            return;
                        }
                    default:
                        return;
                }
            };
        }

    }
}