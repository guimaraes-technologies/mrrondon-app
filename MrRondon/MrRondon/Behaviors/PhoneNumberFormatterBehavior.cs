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
            if (args.OldTextValue == null) return;
            if (args.NewTextValue.Length < args.OldTextValue.Length) return;

            var entry = (Entry) sender;

            entry.Text = FormatCellphoneNumber(entry.Text);
        }

        private static string FormatCellphoneNumber(string input)
        {
            if (input.Length > 15)
            {
                input = input.Remove(input.Length - 1);
            }
            else switch (input.Length)
            {
                case 2:
                    input = "(" + input + ") ";
                    break;
                case 10:
                    input = input + "-";
                    break;
                    default: return input;
            }

            return input;
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