using Foundation;
using CoreGraphics;
using UIKit;

namespace Lab1.iOS.Views
{
    public class CustomCell : UITableViewCell
    {
        private UILabel _titleYearLabel, _topCastLabel;
        private UIImageView _posterView;

        public CustomCell(NSString cellId) : base(UITableViewCellStyle.Default, cellId)
        {
            this._posterView = new UIImageView();
            this._titleYearLabel = new UILabel
            {
                Font = UIFont.FromName("AppleSDGothicNeo-Regular", 16f),
                TextColor = UIColor.FromRGB(85, 5, 0)
            };
            this._topCastLabel = new UILabel
            {
                Font = UIFont.FromName("AppleSDGothicNeo-UltraLight", 10f),
                TextColor = UIColor.FromRGB(85, 5, 0)
            };
            ContentView.AddSubviews(new UIView[] { this._titleYearLabel, this._topCastLabel });
        }

        public void UpdateCell(string title, string year, string cast)
        {
            _titleYearLabel.Text = title + " (" + year + ")";
            _topCastLabel.Text = cast;

            Accessory = UITableViewCellAccessory.DisclosureIndicator;
        }

        public override void LayoutSubviews()
        {
            base.LayoutSubviews();
            _titleYearLabel.Frame = new CGRect(50, 5, ContentView.Bounds.Width - 30, 25);
            _topCastLabel.Frame = new CGRect(50, 25, ContentView.Bounds.Width - 30, 20);
        }
    }
}