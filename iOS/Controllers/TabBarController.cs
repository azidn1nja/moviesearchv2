using System;
using System.Collections.Generic;
using System.Text;

namespace Lab1.iOS.Controllers
{
    using UIKit;
    public class TabBarController : UITabBarController
    {
        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

            ViewControllerSelected += (sender, args) =>
            {
                var controller = args.ViewController;
                if (controller.GetType() == typeof(UINavigationController))
                {
                    var controller2 = (UINavigationController)controller;
                    if (controller2.TopViewController.GetType() == typeof(TopRatedController))
                    {
                        var controller3 = (TopRatedController)controller2.TopViewController;
                        controller3.refreshList();
                    }
                }
            };

            TabBar.BackgroundColor = UIColor.LightGray;
            TabBar.TintColor = UIColor.Red;

            SelectedIndex = 0;
        }
    }
}