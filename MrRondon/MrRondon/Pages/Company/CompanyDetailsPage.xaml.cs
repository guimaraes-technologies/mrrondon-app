using Xamarin.Forms;

namespace MrRondon.Pages.Company
{
	public partial class CompanyDetailsPage : ContentPage
	{
		public CompanyDetailsPage (CompanyDetailsPageModel pageModel)
		{
			InitializeComponent ();
		    NavigationPage.SetHasNavigationBar(this, false);
            BindingContext = pageModel; 
        }
	}
}