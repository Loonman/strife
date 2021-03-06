﻿using System;
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
using Windows.UI.Xaml.Shapes;
using System.Collections.Specialized;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace Strife
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public MainPageViewModel MainPageViewModel { get; set; }
        public DiscordAuthenticator authenticator { get; set; }
        public static CoreDispatcher dispatcher { get; private set; }

        public MainPage()
        {       
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

            dispatcher = Windows.UI.Core.CoreWindow.GetForCurrentThread().Dispatcher;
            var authenticatedRestFactory = new AuthenticatedRestFactory(apiConfig, authenticator);

            var channelService = authenticatedRestFactory.GetChannelService();
            var guildService = authenticatedRestFactory.GetGuildService();
            var userService = authenticatedRestFactory.GetUserService();

            var gateway = new Gateway(gatewayConfig, authenticator);

            await gateway.ConnectAsync();

            var messageStore = new MessageStore(channelService, gateway);

            var guildChannelStore = new GuildChannelStore(guildService, gateway);
            var guildStore = new GuildStore(userService);

            MainPageViewModel = new MainPageViewModel(guildStore, guildChannelStore, messageStore);

            /*var messageProvider = new MessageProvider(messageStore, "81384956881809408");
            var channelProvider = new GuildChannelProvider(guildChannelStore, "81384788765712384");

            var guilds = await guildStore.GetGuildsAsync();
            var channels = await channelStore.GetChannelsAsync(guilds.First().Id);

            var messageProvider = new MessageProvider(messageStore, channels.First().Id);
            var channelProvider = new GuildChannelProvider(channelStore, guilds.First().Id);

            MessagesViewModel = new MessagesViewModel(messageProvider);
            ChannelsViewModel = new ChannelsViewModel(channelProvider);
            GuildsViewModel = new GuildsViewModel(guildStore);*/

            this.InitializeComponent();
        }

        private void OnMessagesListChange(object sender, NotifyCollectionChangedEventArgs e)
        {
            messagesListView.ScrollIntoView(messagesListView.Items[messagesListView.Items.Count - 1]);
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            authenticator = new DiscordAuthenticator((String)e.Parameter);
            InitAsync();
        }

        private void guildIcon_Tapped(object sender, TappedRoutedEventArgs e)
        {
            GuildViewModel guild = ((sender as Ellipse).DataContext as GuildViewModel);
            MainPageViewModel.OnGuildTapped(guild);
        }

        private void StackPanel_Tapped(object sender, TappedRoutedEventArgs e)
        {
            ChannelViewModel guild = ((sender as StackPanel).DataContext as ChannelViewModel);
            MainPageViewModel.OnChannelTapped(guild);
            ((INotifyCollectionChanged)messagesListView.ItemsSource).CollectionChanged += OnMessagesListChange;
        }

        private void AppBarButton_Tapped(object sender, TappedRoutedEventArgs e)
        {
            MySplitView.IsPaneOpen = !MySplitView.IsPaneOpen;
        }

    }
}
   
