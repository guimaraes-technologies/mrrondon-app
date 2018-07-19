using System.Collections;
using System.Windows.Input;
using Xamarin.Forms;

namespace MrRondon.Controls
{
    //https://github.com/mattwhetton/Codenutz.XF.InfiniteListView
    public class InfiniteListView : ListView
    {
        public static readonly BindableProperty LoadMoreCommandProperty = BindableProperty.Create(nameof(LoadMoreCommandProperty), typeof(ICommand), typeof(InfiniteListView), default(ICommand));
        public ICommand LoadMoreCommand
        {
            get => (ICommand)GetValue(LoadMoreCommandProperty);
            set => SetValue(LoadMoreCommandProperty, value);
        }

        public InfiniteListView()
        {
            ItemAppearing += InfiniteListView_ItemAppearing;
        }

        private void InfiniteListView_ItemAppearing(object sender, ItemVisibilityEventArgs e)
        {
            if (!(ItemsSource is IList items) || e.Item != items[items.Count - 1]) return;

            if (LoadMoreCommand != null && LoadMoreCommand.CanExecute(null))
                LoadMoreCommand.Execute(null);
        }
    }
}