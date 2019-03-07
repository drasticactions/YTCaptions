using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.PlatformConfiguration.iOSSpecific;
using YTCaptions.ViewModels;

namespace YTCaptions
{
    public partial class MainPage : ContentPage
    {
        YTSearchViewModel viewModel;
        public MainPage()
        {
            InitializeComponent();
            On<Xamarin.Forms.PlatformConfiguration.iOS>().SetUseSafeArea(true);
            BindingContext = viewModel = new YTSearchViewModel();
        }

        private void OnSearch(object sender, EventArgs args)
        {
            var searchBar = (SearchBar)sender;
            var text = searchBar.Text;
            viewModel.SearchCommand.Execute(null);
        }
    }
}
