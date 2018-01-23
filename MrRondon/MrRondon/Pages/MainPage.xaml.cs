﻿using System;
using System.Threading.Tasks;
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

            CurrentPageChanged += async (sender, e) =>
            {
                var numPage = Children.IndexOf(CurrentPage);

                if (CurrentPage.BindingContext != null) return;

                switch (numPage)
                {
                    case 1:
                        {
                            var service = new CategoryService();
                            var items = await service.GetAsync();
                            CurrentPage.BindingContext = new ListCategoriesPageModel
                            {
                                Items = new ObservableRangeCollection<Entities.Category>(items)
                            };
                            return;
                        }
                    case 2:
                        {
                            var service = new EventService();
                            var items = await service.GetAsync();
                            CurrentPage.BindingContext = new ListEventPageModel
                            {
                                Items = new ObservableRangeCollection<Entities.Event>(items)
                            };
                            return;
                        }
                    case 3:
                        {
                            var service = new HistoricalSightService();
                            var items = await service.GetAsync();
                            CurrentPage.BindingContext = new ListHistoricalSightPageModel
                            {
                                Items = new ObservableRangeCollection<Entities.HistoricalSight>(items)
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