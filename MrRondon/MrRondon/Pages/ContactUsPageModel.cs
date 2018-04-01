using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
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
        private const int MessageMaxLength = 250;
        public ICommand SendMessageCommand { get; set; }

        public ContactUsPageModel()
        {
            Title = "Fale com a gente";
            Subjects = EnumExtensions.ConvertToList<Subject>().ToList();
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
            set
            {
                _message = value;
                var letters = value?.Length ?? 0;
                CountLetter = $"{letters.ToString().PadLeft(3, '0')} / {MessageMaxLength}";
                Notify(nameof(Message));
            }
        }

        private string _countLetter = $"000 / {MessageMaxLength}";
        public string CountLetter
        {
            get => _countLetter;
            set => SetProperty(ref _countLetter, value);
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

                if (!hasBeenSended)
                {
                    await MessageService.ShowAsync("Erro", $"Não foi possível enviar a sua mensagem, mas você pode entrar em contato com a SETUR pelo telefone {AccountManager.DefaultSetting.TelephoneSetur} ou pelo email {AccountManager.DefaultSetting.EmailSetur}.");
                }

                Message = string.Empty;
                Subject = null;
                await MessageService.ToastAsync("Mensagem enviada com sucesso.");
            }
            catch (TaskCanceledException ex)
            {
                Debug.WriteLine(ex);
                await MessageService.ShowAsync("Informação", "A requisição está demorando muito, verifique sua conexão com a internet.");
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
                Console.WriteLine(ex);
                await MessageService.ShowAsync(ex.Message);
            }
            finally
            {
                IsLoading = false;
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
            return Message.Length < MessageMaxLength;
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
        UpdateEvent = 4,
        [EnumValueData(Description = "Sugestão", KeyValue = "5")]
        Suggestion = 5
    }
}