using Strife.Domain.UserStorage;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using Windows.ApplicationModel.Core;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Core;
using Windows.UI.ViewManagement;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace Strife
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class SplashPage : Page
    {
        public SplashPage()
        {
            this.InitializeComponent();
            var userStore = new UserStore();
            if (userStore.hasToken())
            {
                NavigateToMainPage(userStore.getToken());
            }
            else
            {
                NavigateToLoginPage();
            }
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

        private async void NavigateToMainPage(string token)
        {
            var currentId = ApplicationView.GetForCurrentView().Id;
            var view = CoreApplication.CreateNewView();
            await view.Dispatcher.RunAsync(CoreDispatcherPriority.Normal, () =>
            {
                var frame = new Frame();
                frame.Navigate(typeof(MainPage), token);
                Window.Current.Content = frame;
                Window.Current.Activate();

                viewId = ApplicationView.GetForCurrentView().Id;
            });

            await ApplicationViewSwitcher.SwitchAsync(viewId, currentId);
        }
    }
}
