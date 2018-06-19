using System;
using Navigation.CustomUIElements;
using Navigation.iOS.Renderers;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(TextCell), typeof(TextCellWithCustomBackground))]
namespace Navigation.iOS.Renderers
{
    public class TextCellWithCustomBackground : TextCellRenderer
    {
        public override UITableViewCell GetCell(Cell item, UITableViewCell reusableCell, UITableView tv)
        {
            var cell = base.GetCell(item, reusableCell, tv);

            cell.SelectedBackgroundView = new UIView
            {
                BackgroundColor = new UIColor(red: 0.08f, green: 0.31f, blue: 0.27f, alpha: 1.0f)
            };
            return cell;
        }
    }
}
