using Strife.Domain.GuildChannelStorage;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Strife.ViewModels
{
    public class ChannelsViewModel : NotificationBase
    {
        public ObservableCollection<ChannelViewModel> TextChannels { get; set; } = new ObservableCollection<ChannelViewModel>();

        public ObservableCollection<ChannelViewModel> VoiceChannels { get; set; } = new ObservableCollection<ChannelViewModel>();

        private readonly GuildChannelProvider _guildChannelProvider;

        public ChannelsViewModel(GuildChannelProvider guildChannelProvider)
        {
            _guildChannelProvider = guildChannelProvider;

            LoadBaseChannels();
        }

        public async void LoadBaseChannels()
        {
            var channels = await _guildChannelProvider.GetChannelsAsync();

            channels.Select(c => ChannelViewModel.FromGuildChannel(c))
                    .Where(c => c.Type == "voice")
                    .ToList()
                    .ForEach(c => VoiceChannels.Add(c));

            channels.Select(c => ChannelViewModel.FromGuildChannel(c))
                    .Where(c => c.Type == "text")
                    .ToList()
                    .ForEach(c => TextChannels.Add(c));
        }
    }
}
