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

	    private void PlaceUntil_OnValueChanged(object sender, ValueChangedEventArgs e)
	    {
	        _pageModel.SetValue(e.NewValue);
	    }
	}
}