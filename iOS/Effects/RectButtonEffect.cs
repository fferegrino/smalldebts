using System;
using Smalldebts.iOS.Effects;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ResolutionGroupName("Effects")]
[assembly: ExportEffect(typeof(RectButtonEffect), nameof(RectButtonEffect))]
namespace Smalldebts.iOS.Effects
{
	public class RectButtonEffect : PlatformEffect
	{
		protected override void OnAttached()
		{
			Control.Layer.CornerRadius = (nfloat)0.0;
		}

		protected override void OnDetached()
		{
		}
	}
}