using System;
using System.Windows.Input;
using MrRondon.Services;
using Xamarin.Forms;

namespace MrRondon.Pages.Account
{
    public class RegisterPageModel : BasePageModel
    {
        public RegisterPageModel()
        {
            RegisterCommand = new Command(ExecuteRegister);
            Title = "Registrar";
        }

        private string _firstName;
        public string FirstName
        {
            get => _firstName;
            set => SetProperty(ref _firstName, value);
        }

        private string _lastName;
        public string LastName
        {
            get => _lastName;
            set => SetProperty(ref _lastName, value);
        }

        private string _cpf;
        public string Cpf
        {
            get => _cpf;
            set => SetProperty(ref _cpf, value);
        }

        private string _mail;
        public string Email
        {
            get => _mail;
            set => SetProperty(ref _mail, value);
        }

        private string _password;
        public string Password
        {
            get => _password;
            set => SetProperty(ref _password, value);
        }

        private string _confirmPassword;
        public string ConfirmPassword
        {
            get => _confirmPassword;
            set => SetProperty(ref _confirmPassword, value);
        }

        private string _cellphone;
        public string CellPhone
        {
            get => _telephone;
            set => SetProperty(ref _cellphone, value);
        }

        private string _telephone;
        public string Telephone
        {
            get => _telephone;
            set => SetProperty(ref _telephone, value);
        }
        public ICommand RegisterCommand { get; set; }

        private async void ExecuteRegister()
        {
            try
            {
                if (IsLoading) return;
                IsLoading = true;
                var service = new UserService();
                await service.Register(this);
                await NavigationService.PopModalAsync();
            }
            catch (Exception ex)
            {
                await MessageService.ShowAsync(ex.Message);
            }
            finally
            {
                IsLoading = false;
            }
        }
    }
}