using Smalldebts.Droid.Effects;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ResolutionGroupName("Effects")]
[assembly: ExportEffect(typeof(RectButtonEffect), nameof(RectButtonEffect))]
namespace Smalldebts.Droid.Effects
{
    public class RectButtonEffect : PlatformEffect
    {
        protected override void OnAttached()
        {
            var xamarinButton = Element as Button;
            Control.SetBackgroundColor(xamarinButton.BackgroundColor.ToAndroid());
        }

        protected override void OnDetached()
        {
        }
    }
}