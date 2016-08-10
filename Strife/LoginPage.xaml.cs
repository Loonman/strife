using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using Windows.UI.Notifications;
using Windows.Data.Xml.Dom;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace Strife
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class LoginPage : Page
    {
        MainPage rootPage = MainPage.Current;
        public LoginPage()
        {
            this.InitializeComponent();
        }

        private void strifeLogin_PointerReleased(object sender, RoutedEventArgs e)
        {
            var txtBox = ((Windows.UI.Xaml.Controls.TextBox)sender);
            if (txtBox.Text == "Enter your e-mail")
            {
                txtBox.Text = "";
            }
        }

        private void loginButton_Click(object sender, RoutedEventArgs e)
        {

        }


        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            string xml = $@"<toast activationType='foreground'>
                                            <visual>
                                                <binding template='ToastGeneric'>
                                                </binding>
                                            </visual>
                                        </toast>";

            XmlDocument doc = new XmlDocument();
            doc.LoadXml(xml);

            var binding = doc.SelectSingleNode("//binding");

            var el = doc.CreateElement("text");
            el.InnerText = "Toasties";

            binding.AppendChild(el);

            el = doc.CreateElement("text");
            el.InnerText = "Toasts are popping now boiz";
            binding.AppendChild(el);

            var toast = new ToastNotification(doc);

            ToastNotificationManager.CreateToastNotifier().Show(toast);

        }

    }
}
