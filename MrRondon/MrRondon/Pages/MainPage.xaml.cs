using System;
using System.Diagnostics;
using System.Threading.Tasks;
using MrRondon.Helpers;
using MrRondon.Pages.Event;
using MrRondon.Pages.Map;
using Xamarin.Forms;
using Xamarin.Forms.Maps;

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
                            var pageModel = new ListEventPageModel();
                            pageModel.LoadCitiesCommand.Execute(null);
                            pageModel.LoadItemsCommand.Execute(null);

                            CurrentPage.BindingContext = pageModel;
                            return;
                        }
                    case 2:
                        {
                            //var pageModel = new MapPageModel();
                            //CurrentPage.BindingContext = pageModel;
                            return;
                        }
                    default:
                        return;
                }
            };
        }
    }
}