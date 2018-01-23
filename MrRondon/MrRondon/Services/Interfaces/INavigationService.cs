using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace MrRondon.Services.Interfaces
{
	public interface INavigationService
	{
        void NavigateToUrl(string url);
	    Task PushModalAsync(Page page);
        Task PushAsync(Page page);
	    Task PopAsync();
	    Task PopModalAsync();
		void RemovePage(Type type);
	    void RemovePage(Page page);
		IList<Page> GetNavigationStack();
	    Page GetCurrentPage();
	}
}