using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using MrRondon.Extensions;
using MrRondon.Helpers;
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

        private Subject? _subject;
        public Subject? Subject
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
                Validate();
                var builder = new EmailMessageBuilder()
                    .To(Auth.AccountManager.DefaultSetting.EmailSetur)
                    .Subject(EnumExtensions.GetEnumAttribute(Subject).Description)
                    .BodyAsHtml(Message).Build();

                var emailMessenger = CrossMessaging.Current.EmailMessenger;
                if (emailMessenger.CanSendEmail) emailMessenger.SendEmail(builder);
                await MessageService.ToastAsync("Mensagem enviada com sucesso.");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                await MessageService.ShowAsync("Erro", ex.Message);
            }
        }

        public bool Validate()
        {
            if (string.IsNullOrWhiteSpace(Name)) throw new Exception("O campo Nome é obrigatório.");
            if (string.IsNullOrWhiteSpace(Email)) throw new Exception("O campo Email é obrigatório.");
            if(!EmailHelper.IsEmail(Email)) throw new Exception("O Email informado é inválido.");
            if (string.IsNullOrWhiteSpace(Cellphone) && string.IsNullOrWhiteSpace(Telephone))
                throw new Exception("É obrigatório informar pelo menos um número para contato");

            if (Subject == null) throw new Exception("O campo Assunto é obrigatório.");

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