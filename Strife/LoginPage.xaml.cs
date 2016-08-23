using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
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
using login = Discord_UWP.API.Login;
using auth = Discord_UWP.Authentication;

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

        private async void loginButton_Click(object sender, RoutedEventArgs e)
        {
            var password = strifePassword.Password;
            var userName = strifeLogin.Text;

            try
            {
                login.Models.LoginResult result = await this.sendLoginRequest(userName, password);
                loginError.Text = $"You did it nerd: {result.Token}";
            }
            catch
            {
                loginError.Text = "Login failed: invalid username or password";
            }

           
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

        private async Task<login.Models.LoginResult> sendLoginRequest(string email, string password)
        {

            var config = new Discord_UWP.API.DiscordApiConfiguration
            {
                BaseUrl = "https://discordapp.com/api"
            };

            var authenticator = new auth.DiscordAuthenticator("I'm some george bullshit");
            var restFactory = new Discord_UWP.API.RestFactory(config, authenticator);

            login.ILoginService loginService = restFactory.GetLoginService();

            var loginAPIRequest = new login.Models.LoginRequest
            {
                Email = email,
                Password = password
            };

            return await loginService.Login(loginAPIRequest);
        }

    }
}
