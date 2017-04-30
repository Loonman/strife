using Discord_UWP.Authentication;
using Discord_UWP.SharedModels;
using Strife.Domain.GuildChannelStorage;
using Strife.Domain.GuildStorage;
using Strife.Domain.MessageStorage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Shapes;

namespace Strife.ViewModels
{
    public class MainPageViewModel : NotificationBase
    {
        private readonly GuildStore _guildStore;
        private readonly GuildChannelStore _guildChannelStore;
        private readonly MessageStore _messageStore;

        private GuildsViewModel guildsViewModel;
        private ChannelsViewModel channelsViewModel;
        private MessagesViewModel messagesViewModel;

        public GuildsViewModel GuildsViewModel
        {
            get { return guildsViewModel; }
            set { SetProperty(ref guildsViewModel, value); }
        }

        public ChannelsViewModel ChannelsViewModel
        {
            get { return channelsViewModel; }
            set { SetProperty(ref channelsViewModel, value); }
        }

        public MessagesViewModel MessagesViewModel
        {
            get { return messagesViewModel; }
            set { SetProperty(ref messagesViewModel, value); }
        }

        public MainPageViewModel(GuildStore guildStore, GuildChannelStore guildChannelStore, MessageStore messageStore)
        {
            _guildStore = guildStore;
            _guildChannelStore = guildChannelStore;
            _messageStore = messageStore;

            GuildsViewModel = new GuildsViewModel(guildStore);
        }

        public void OnGuildTapped(GuildViewModel guildViewModel)
        {
            var channelProvider = new GuildChannelProvider(_guildChannelStore, guildViewModel.Id);
            ChannelsViewModel = new ChannelsViewModel(channelProvider);
        }

        public void OnChannelTapped(ChannelViewModel textChannelViewModel)
        {
            var messageProvider = new MessageProvider(_messageStore, textChannelViewModel.Id);
            MessagesViewModel = new MessagesViewModel(messageProvider);
        }
    }
}
