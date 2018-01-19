using Xamarin.Forms;

namespace MrRondon.Behaviors
{
    public class ToUpperCaseBehavior : BehaviorBase<Entry>
    {
        private static readonly BindablePropertyKey IsUpperCasePropertyKey = BindableProperty.CreateReadOnly("IsUpperCase", typeof(bool), typeof(ToUpperCaseBehavior), false);

        public static readonly BindableProperty IsUpperCaseProperty = IsUpperCasePropertyKey.BindableProperty;

        //Método responsável por vincular o evento de "TextChanged" ao Entry
        protected override void OnAttachedTo(Entry bindable)
        {
            bindable.TextChanged += Entry_TextChanged;
            base.OnAttachedTo(bindable);
        }

        //Método responsável por desvincular o evento de "TextChanged" ao Entry
        protected override void OnDetachingFrom(Entry bindable)
        {
            bindable.TextChanged -= Entry_TextChanged;
            base.OnDetachingFrom(bindable);
        }

        //Método responsável por executar a validação do campo
        private static void Entry_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (!(sender is Entry entry)) return;
            entry.Text = string.IsNullOrWhiteSpace(e.NewTextValue) ? string.Empty : e.NewTextValue.ToUpper();
        }
    }
}