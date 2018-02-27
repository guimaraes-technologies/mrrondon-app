using System;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace MrRondon.Pages.City
{
    public class ChangeCityPageModel : BasePageModel
    {
        public ChangeCityPageModel(Page previousPage)
        {
            Title = "Alterar a cidade";
            PreviousPage = previousPage;
            LoadCitiesCommand = new Command(async () => await ExecuteLoadCities());
            ItemSelectedCommand = new Command<Entities.City>(async (item) => await ExecuteItemSelected(item));
        }

        public Page PreviousPage { get; private set; }
        public ICommand ItemSelectedCommand { get; set; }

        private async Task ExecuteItemSelected(Entities.City model)
        {
            try
            {
                if (IsLoading) return;
                IsLoading = true;
                model.SetCity();
                await NavigationService.PopAsync();
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
                await NavigationService.PushAsync(new ErrorPage(new ErrorPageModel(ex.Message, Title)));
            }
            finally
            {
                IsLoading = false;
                IsPresented = false;
            }
        }
    }
}