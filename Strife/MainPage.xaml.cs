using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.ViewManagement;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using Discord_UWP.SharedModels;
using Discord_UWP.API;
using Discord_UWP.Authentication;
using Discord_UWP.Gateway;
using Windows.ApplicationModel.Core;
using Windows.UI.Core;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace Strife
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public static MainPage Current;

        public MainPage()
        {
            
            this.InitializeComponent();

            Current = this;
            NavigateToLoginPage();
        }


        private int viewId;
        private async void NavigateToLoginPage()
        {
           
            var currentId = ApplicationView.GetForCurrentView().Id;
            var view = CoreApplication.CreateNewView();
            await view.Dispatcher.RunAsync(CoreDispatcherPriority.Normal, () =>
            {
                var frame = new Frame();
                frame.Navigate(typeof(LoginPage));
                Window.Current.Content = frame;
                Window.Current.Activate();

                viewId = ApplicationView.GetForCurrentView().Id;
            });
            
            await ApplicationViewSwitcher.SwitchAsync(viewId, currentId);
        }

    }
}
