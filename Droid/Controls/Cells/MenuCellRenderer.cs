using Android.Widget;
using Smalldebts.Core.UI.Controls.Cells;
using Smalldebts.Droid.Controls.Cells;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ExportRenderer(typeof(MenuCell), typeof(MenuCellRenderer))]
namespace Smalldebts.Droid.Controls.Cells
{
    public class MenuCellRenderer : ImageCellRenderer
    {
        protected override Android.Views.View GetCellCore(Cell item, Android.Views.View convertView, Android.Views.ViewGroup parent, Android.Content.Context context)
        {

            LinearLayout cell = (LinearLayout)base.GetCellCore(item, convertView, parent, context);

            ImageView image = (ImageView)cell.GetChildAt(0);
            image.SetScaleType(ImageView.ScaleType.Center);
            return cell;
        }
    }
}