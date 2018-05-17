using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using MrRondon.Auth;
using MrRondon.Entities;
using MrRondon.Helpers;
using Plugin.ExternalMaps;
using Plugin.ExternalMaps.Abstractions;
using Plugin.Share;
using Plugin.Share.Abstractions;
using Xamarin.Forms;

namespace MrRondon.Pages.Company
{
    public class CompanyDetailsPageModel : BasePageModel
    {
        public CompanyDetailsPageModel(Entities.Company model)
        {
            Title = "Detalhes da Empresa";
            Company = model;
            NotHasContacts = Company?.Contacts == null || !Company.Contacts.Any();
            OpenMapCommand = new Command(ExecuteOpenMap);
            ShareCommand = new Command(async () => await ExecuteShare());
            MakePhoneCallCommand = new Command(ExecuteMakePhoneCall);
        }

        public ICommand MakePhoneCallCommand { get; set; }
        public ICommand OpenMapCommand { get; set; }
        public ICommand ShareCommand { get; set; }

        public bool NotHasContacts { get; }

        private Entities.Company _company;
        public Entities.Company Company
        {
            get => _company;
            set => SetProperty(ref _company, value);
        }

        private void ExecuteOpenMap()
        {
            CrossExternalMaps.Current.NavigateTo(Company.Name, Company.Address.Latitude, Company.Address.Longitude, NavigationType.Driving);
        }

        private async Task ExecuteShare()
        {
            var message = new ShareMessage
            {
                Title = Constants.AppName,
                Text = $"Olha o que eu encontrei no {Constants.AppName}:\nEmpresa: {Company.Name}\nLocal: {Company.Address.FullAddressInline}\nMuito TOP, dá uma olhada ;)",
                Url = Constants.SystemUrl
            };
            await CrossShare.Current.Share(message);
        }

        private void ExecuteMakePhoneCall()
        {
            var contact = (Company.Contacts?.FirstOrDefault(f => f.ContactType == ContactType.Cellphone)?.Description ?? Company.Contacts?.FirstOrDefault(f => f.ContactType == ContactType.Telephone)?.Description) ?? Constants.TelephoneSetur;

            NavigationService.MakePhoneCall(contact);
        }
    }
}