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
                ExceptionService.TrackError(ex);
                await MessageService.ShowAsync(ex);
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
            catch (TaskCanceledException ex)
            {
                ExceptionService.TrackError(ex);
                await MessageService.ShowAsync("Informação", "A requisição está demorando muito, verifique sua conexão com a internet.");
            }
            catch (Exception ex)
            {
                ExceptionService.TrackError(ex);
                await MessageService.ShowAsync(ex);
            }
            finally
            {
                IsLoading = false;
                IsPresented = false;
            }
        }
    }
}