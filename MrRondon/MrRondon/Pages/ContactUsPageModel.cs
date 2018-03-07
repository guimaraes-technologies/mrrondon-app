using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Input;
using MrRondon.Auth;
using MrRondon.Extensions;
using MrRondon.Helpers;
using MrRondon.Services;
using MrRondon.ViewModels;
using Plugin.Messaging;
using Xamarin.Forms;

namespace MrRondon.Pages
{
    public class ContactUsPageModel : BasePageModel
    {
        public ICommand SendMessageCommand { get; set; }

        public ContactUsPageModel()
        {
            Title = "Fale Conosco";
            Subjects = EnumExtensions.ConvertEnumToList<Subject>().ToList();
            SendMessageCommand = new Command(ExecuteSendMessage);
        }

        private string _name;
        public string Name
        {
            get => _name;
            set => SetProperty(ref _name, value);
        }

        private string _mail;
        public string Email
        {
            get => _mail;
            set => SetProperty(ref _mail, value);
        }

        private string _cellphone;
        public string Cellphone
        {
            get => _cellphone;
            set => SetProperty(ref _cellphone, value);
        }

        private string _telephone;
        public string Telephone
        {
            get => _telephone;
            set => SetProperty(ref _telephone, value);
        }

        private EnumValueDataAttribute _subject;
        public EnumValueDataAttribute Subject
        {
            get => _subject;
            set => SetProperty(ref _subject, value);
        }

        private List<EnumValueDataAttribute> _subjects;
        public List<EnumValueDataAttribute> Subjects
        {
            get => _subjects;
            set => SetProperty(ref _subjects, value);
        }

        private string _message;
        public string Message
        {
            get => _message;
            set => SetProperty(ref _message, value);
        }

        private async void ExecuteSendMessage()
        {
            try
            {
                if (IsLoading) return;
                IsLoading = true;
                Validate();

                var contactMessage = new ContactMessageVm
                {
                    Name = Name,
                    Telephone = string.IsNullOrWhiteSpace(Telephone) ? "Não informado" : Telephone,
                    Cellphone = string.IsNullOrWhiteSpace(Cellphone) ? "Não informado" : Cellphone,
                    Email = Email.Trim(),
                    Subject = Subject.Description,
                    Message = Message
                };
                var service = new ContactService();
                var hasBeenSended = await service.SendAsync(contactMessage);

                IsLoading = false;
                if (!hasBeenSended)
                {
                    await MessageService.ShowAsync("Erro", $"Não foi possível enviar a sua mensagem, mas você pode entrar em contato com a SETUR pelo telefone {AccountManager.DefaultSetting.TelephoneSetur} ou pelo email {AccountManager.DefaultSetting.EmailSetur}.");
                }

                await MessageService.ToastAsync("Mensagem enviada com sucesso.");

                //var builder = new EmailMessageBuilder()
                //    .To(AccountManager.DefaultSetting.EmailSetur)
                //    .Subject(Subject.Description)
                //    .BodyAsHtml(Message).Build();

                //var emailMessenger = CrossMessaging.Current.EmailMessenger;
                //if (emailMessenger.CanSendEmail) emailMessenger.SendEmail(builder);
                //await MessageService.ToastAsync("Mensagem enviada com sucesso.");
            }
            catch (Exception ex)
            {
                IsLoading = false;
                Console.WriteLine(ex);
                await MessageService.ShowAsync("Erro", ex.Message);
            }
        }

        public bool Validate()
        {
            if (string.IsNullOrWhiteSpace(Name)) throw new Exception("O campo Nome é obrigatório.");
            if (string.IsNullOrWhiteSpace(Email)) throw new Exception("O campo Email é obrigatório.");
            if (!EmailHelper.IsEmail(Email.Trim())) throw new Exception("O Email informado é inválido.");
            if (string.IsNullOrWhiteSpace(Cellphone) && string.IsNullOrWhiteSpace(Telephone))
                throw new Exception("É obrigatório informar pelo menos um número para contato");

            if (Subject == null) throw new Exception("O campo Assunto é obrigatório.");
            if (string.IsNullOrWhiteSpace(Message)) throw new Exception("O campo Mensagem é obrigatório.");

            if (Message.Length < 25) throw new Exception($"Para um melhor entendimento do(a) {Subject.Description}, informe uma mensagem mais detalhada.");

            return true;
        }
    }

    public enum Subject
    {
        [EnumValueData(Description = "Cadastro de empresa", KeyValue = "1")]
        NewCompany = 1,
        [EnumValueData(Description = "Atualização de empresa", KeyValue = "2")]
        UpdateCompany = 2,
        [EnumValueData(Description = "Cadastro de evento", KeyValue = "3")]
        NewEvent = 3,
        [EnumValueData(Description = "Atualização de evento", KeyValue = "4")]
        UpdateEvent = 4
    }
}