using Xamarin.Forms;

namespace MrRondon.Pages.Account
{
	public partial class ConfigurationPage : ContentPage
	{
	    private readonly ConfigurationPageModel _pageModel;

		public ConfigurationPage ()
		{
			InitializeComponent ();
            BindingContext = _pageModel = new ConfigurationPageModel();
		}
	}
}