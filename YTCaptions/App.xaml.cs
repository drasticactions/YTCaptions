using System;
using LibVLCSharp.Shared;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace YTCaptions
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();
            Core.Initialize();
            MainPage = new NavigationPage(new SearchVideosPage());
        }

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}
