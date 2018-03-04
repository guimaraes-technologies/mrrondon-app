using System.Text.RegularExpressions;
using Xamarin.Forms;

namespace MrRondon.Behaviors
{
    public class PhoneNumberFormatterBehavior : Behavior<Entry>, IContactType
    {
        public ContactType Type { get; set; } = ContactType.Cellphone;

        protected override void OnAttachedTo(Entry bindable)
        {
            bindable.TextChanged += OnTextChanged;

            base.OnAttachedTo(bindable);
        }

        protected override void OnDetachingFrom(Entry bindable)
        {
            bindable.TextChanged -= OnTextChanged;

            base.OnDetachingFrom(bindable);
        }

        private void OnTextChanged(object sender, TextChangedEventArgs args)
        {
            var entry = (Entry)sender;

            entry.Text = Type == ContactType.Cellphone ? FormatCellphoneNumber(entry.Text) : FormatTelephoneNumber(entry.Text);
        }

        private static string FormatCellphoneNumber(string input)
        {
            var digitsRegex = new Regex(@"[^\d]");
            var digits = digitsRegex.Replace(input, "");

            if (digits.Length <= 1) return $"({digits}";
            if (digits.Length == 2) return $"({digits.Substring(0, 2)}) ";

            var lenght = digits.Length;
            if (digits.Length > 2 && digits.Length < 7) return $"({digits.Substring(0, 2)}) {digits.Substring(2, lenght - 2)}";

            if (digits.Length >= 7 && digits.Length < 11)
                return $"({digits.Substring(0, 2)}) {digits.Substring(2, 5)}-{digits.Substring(7, lenght - 7)}";

            return $"({digits.Substring(0, 2)}) {digits.Substring(2, 5)}-{digits.Substring(7, 4)}";
        }

        private static string FormatTelephoneNumber(string input)
        {
            var digitsRegex = new Regex(@"[^\d]");
            var digits = digitsRegex.Replace(input, "");

            if (digits.Length <= 1) return $"({digits}";
            if (digits.Length == 2) return $"({digits.Substring(0, 2)}) ";

            var lenght = digits.Length;
            if (digits.Length > 2 && digits.Length < 6) return $"({digits.Substring(0, 2)}) {digits.Substring(2, lenght - 2)}";

            if (digits.Length >= 6 && digits.Length < 11)
                return $"({digits.Substring(0, 2)}) {digits.Substring(2, 4)}-{digits.Substring(6, lenght - 6)}";

            return $"({digits.Substring(0, 2)}) {digits.Substring(2, 4)}-{digits.Substring(6, 4)}";
        }
    }

    public interface IContactType
    {
        ContactType Type { get; set; }
    }

    public enum ContactType
    {
        Telephone,
        Cellphone
    }
}