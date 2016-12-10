using Android.App;
using Android.Widget;
using Android.OS;
using Android.Provider;
using Lab1.MovieDbConnection;
using Lab1.Models;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using System.IO;
using Android.Support.Design.Widget;
using Android.Support.V4.App;
using Android.Support.V4.View;

namespace Lab1.Droid
{
    using Android.Content;
    using Android.Hardware.Input;
    using Android.Runtime;
    using Android.Views;
    using Android.Views.InputMethods;
    using MovieDownload;
    using System.Threading;

    [Activity(Theme = "@style/MovieSearchTheme" ,Label = "Lab1", Icon = "@drawable/icon")]
	public class MainActivity : FragmentActivity
	{

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.Main);

            var fragments = new Android.Support.V4.App.Fragment[]
                {
                    new MovieSearchInputFragment()
                };
            var titles = CharSequence.ArrayFromStringArray(new[]{"Movies"});

            var viewPager = this.FindViewById<ViewPager>(Resource.Id.viewpager);
            viewPager.Adapter = new TabsFragmentPagerAdapter(SupportFragmentManager, fragments, titles);

            var tabLayout = this.FindViewById<TabLayout>(Resource.Id.sliding_tabs);
            tabLayout.SetupWithViewPager(viewPager);

            var toolbar = this.FindViewById<Toolbar>(Resource.Id.toolbar);
            this.SetActionBar(toolbar);
            this.ActionBar.Title = GetString(Resource.String.ToolbarTitle);
        }
	}
}

