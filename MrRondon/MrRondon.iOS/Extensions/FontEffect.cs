using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ResolutionGroupName("MrRondon")]
[assembly: ExportEffect(typeof(MrRondon.iOS.Extensions.FontEffect), "FontEffect")]
namespace MrRondon.iOS.Extensions
{
    public class FontEffect : PlatformEffect
    {
        protected override void OnAttached()
        {
            var textView = (UITextField)Control;

            textView.Font = UIFont.FromName("Avenir-LightOblique", 24);
        }

        protected override void OnDetached()
        {
        }
    }
}