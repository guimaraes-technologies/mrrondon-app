using DLToolkit.Forms.Controls;
using MrRondon.Droid.Renderers;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;
using ListView = Android.Widget.ListView;

[assembly: ExportRenderer(typeof(FlowListViewInternalCell), typeof(FlowListViewInternalCellRenderer))]
namespace MrRondon.Droid.Renderers
{
    // DISABLES FLOWLISTVIEW ROW HIGHLIGHT
    public class FlowListViewInternalCellRenderer : ViewCellRenderer
    {
        protected override Android.Views.View GetCellCore(Cell item, Android.Views.View convertView, Android.Views.ViewGroup parent, Android.Content.Context context)
        {
            var cell = base.GetCellCore(item, convertView, parent, context);

            if (!(parent is ListView listView)) return cell;
            listView.SetSelector(Android.Resource.Color.Transparent);
            listView.CacheColorHint = Android.Graphics.Color.Transparent;

            return cell;
        }
    }
}