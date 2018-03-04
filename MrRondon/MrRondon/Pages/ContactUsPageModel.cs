using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using MrRondon.Extensions;
using MrRondon.Helpers;
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

        private Subject _subject;
        public Subject Subject
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

        private void ExecuteSendMessage()
        {
            try
            {

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                throw;
            }
        }
    }

    public enum Subject
    {
        [EnumValueData(Description = "Cadastro de empresa")]
        NewCompany = 1,
        [EnumValueData(Description = "Atualização de empresa")]
        UpdateCompany = 2,
        [EnumValueData(Description = "Cadastro de evento")]
        NewEvent = 3,
        [EnumValueData(Description = "Atualização de evento")]
        UpdateEvent = 4
    }
}