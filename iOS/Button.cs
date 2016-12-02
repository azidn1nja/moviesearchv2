using UIKit;
using Foundation;
using CoreGraphics;

namespace Lab1.iOS
{
    [Register("Button")]
    public class Button : UIButton
    {
        protected void SetStylesForState(UIControlState state)
        {
            switch (state)
            {
                case UIControlState.Normal:
                    {
                        Layer.BackgroundColor = new CGColor(255, 255, 255);
                        Layer.BorderColor = new CGColor(0, 0, 0, 0.5f);
                    break;
                    }
                case UIControlState.Highlighted:
                    {
                        Layer.BackgroundColor = new CGColor(0, 0, 0, 0.1f);
                        Layer.BorderColor = new CGColor(0, 0, 0, 0.3f);
                    break;
                    }
            }
        }
        public override bool Enabled
        {
            get
            {
                return base.Enabled;
            }
            set
            {
                base.Enabled = value;
                var state = value ? UIControlState.Normal : UIControlState.Disabled;
                SetStylesForState(state);
            }
        }
        public override bool Highlighted
        {
            get
            {
                return base.Highlighted;
            }
            set
            {
                base.Highlighted = value;
                var state = value ? UIControlState.Highlighted : UIControlState.Normal;
                SetStylesForState(state);
            }
        }
    }
}