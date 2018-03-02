using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using MrRondon.Auth;
using MrRondon.Helpers;
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
                CurrentCity = model;
                ApplicationManager<Entities.City>.AddOrUpdate("city", model);
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

        protected async Task ExecuteLoadCities()
        {
            try
            {
                if (IsLoading) return;
                IsLoading = true;
                var items = await AccountManager.GetHasHistoricalSightAsync();
                Cities.ReplaceRange(items);
                CityNames = new List<string>(items.Select(s => s.Name));

               //todo FINISH
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
                await NavigationService.PushAsync(new ErrorPage(new ErrorPageModel(ex.Message, Title) { IsLoading = false }));
            }
            finally
            {
                IsLoading = false;
                IsPresented = false;
            }
        }
    }
}