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
using Strife.ViewModels;
using Strife.Domain;
using Strife.Domain.MessageStorage;
using Strife.Domain.GuildChannelStorage;
using Strife.Domain.GuildStorage;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace Strife
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public GuildsViewModel GuildsViewModel { get; set; }
        public ChannelsViewModel ChannelsViewModel { get; set; }
        public MessagesViewModel MessagesViewModel { get; set; }

        public MainPage()
        {
            InitAsync();            
        }

        public async void InitAsync()
        {
            var apiConfig = new DiscordApiConfiguration
            {
                BaseUrl = "https://discordapp.com/api"
            };

            var gatewayConfig = new GatewayConfig
            {
                BaseUrl = "wss://gateway.discord.gg/"
            };

            var authenticator = new DiscordAuthenticator("PUT TOKEN HERE");
            var authenticatedRestFactory = new AuthenticatedRestFactory(apiConfig, authenticator);

            var channelService = authenticatedRestFactory.GetChannelService();
            var guildService = authenticatedRestFactory.GetGuildService();
            var userService = authenticatedRestFactory.GetUserService();

            var gateway = new Gateway(gatewayConfig, authenticator);

            await gateway.ConnectAsync();

            var messageStore = new MessageStore(channelService, gateway);
            var messageProvider = new MessageProvider(messageStore, "81384956881809408");

            var channelStore = new GuildChannelStore(guildService, gateway);
            var channelProvider = new GuildChannelProvider(channelStore, "81384788765712384");

            var guildStore = new GuildStore(userService);

            MessagesViewModel = new MessagesViewModel(messageProvider);
            ChannelsViewModel = new ChannelsViewModel(channelProvider);
            GuildsViewModel = new GuildsViewModel(guildStore);

            this.InitializeComponent();
        }
    }
}
   
