using System.Text.RegularExpressions;
using Xamarin.Forms;

namespace MrRondon.Behaviors
{
    public class PhoneNumberFormatterBehavior : Behavior<Entry>
    {
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

        private static void OnTextChanged(object sender, TextChangedEventArgs args)
        {
            var entry = (Entry)sender;
            
            entry.Text = FormatPhoneNumber(entry.Text);
        }

        private static string FormatPhoneNumber(string input)
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
    }
}