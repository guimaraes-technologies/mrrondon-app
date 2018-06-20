using System.Text.RegularExpressions;
using Xamarin.Forms;

namespace MrRondon.Behaviors
{
    public class CpfValidatorBehavior : Behavior<Entry>
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
            if (string.IsNullOrWhiteSpace(args.NewTextValue)) return;
            var isValid = IsValidCpf(args.NewTextValue);

            ((Entry)sender).TextColor = isValid ? Color.Default : Color.Red;
        }

        public static bool IsValidCpf(string input)
        {
            var digitsRegex = new Regex(@"[^\d]");
            var digits = digitsRegex.Replace(input, "");
            return digits.Length == 11;
        }
    }
}