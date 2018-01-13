using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MrRondon.Services.Interfaces;
using Xamarin.Forms;

namespace MrRondon.Services
{
    public class NavigationService : INavigationService
    {
        public void NavigateToUrl(string url)
        {
            Device.OpenUri(new Uri(url));
        }

        public async Task PushAsync(Page page)
        {
            await Application.Current.MainPage.Navigation.PushAsync(page);
        }

        public async Task PushModalAsync(Page page)
        {
            await Application.Current.MainPage.Navigation.PushModalAsync(page);
        }

        public async Task PopAsync()
        {
            await Application.Current.MainPage.Navigation.PopAsync();
        }

        public async Task PopModalAsync()
        {
            await Application.Current.MainPage.Navigation.PopModalAsync();
		}

		public void RemovePage(Type type)
		{
			var page = (Page)Activator.CreateInstance(type);
			foreach (var item in GetNavigationStack())
			{
				if(item == page) Application.Current.MainPage.Navigation.RemovePage(page);
			}
		}

		public void RemovePage(Page page)
		{
			foreach (var item in GetNavigationStack())
			{
				if (item == page) Application.Current.MainPage.Navigation.RemovePage(page);
			}
		}

		public IList<Page> GetNavigationStack()
		{
			return Application.Current.MainPage.Navigation.NavigationStack.ToList();
		}
    }
}