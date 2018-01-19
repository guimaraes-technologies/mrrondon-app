using Xamarin.Forms;

namespace MrRondon.Extensions
{
    //https://github.com/angelobelchior/XFCustomControls/blob/master/XFCustomControls.Ext/Entry.cs
    public class Entry : Xamarin.Forms.Entry
    {
        public static readonly BindableProperty MaxLengthProperty = BindableProperty.Create("MaxLength", typeof (int?), typeof (Entry), 
                default(int?), BindingMode.TwoWay, 
                propertyChanged: (bindable, oldvalue, newvalue) =>
                {
                    var entry = (Entry) bindable;
                    entry.TruncateText();
                });

        public int? MaxLength
        {
            get => (int?) GetValue(MaxLengthProperty);
            set => SetValue(MaxLengthProperty, value);
        }

        public Entry()
        {
            TextChanged += (sender, e) => TruncateText();
        }

        private void TruncateText()
        {
            if (string.IsNullOrWhiteSpace(Text)) return;
            if (!MaxLength.HasValue) return;

            if (Text.Length <= MaxLength.GetValueOrDefault()) return;
            var maxLength = MaxLength.GetValueOrDefault();
            var value = Text;
            value = value.Remove(value.Length - (value.Length - maxLength));
            Text = value;
        }
    }
}