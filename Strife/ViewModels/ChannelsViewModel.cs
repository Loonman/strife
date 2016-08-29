using Strife.Domain.GuildChannelStorage;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Strife.ViewModels
{
    public class ChannelsViewModel
    {
        public ObservableCollection<TextChannelViewModel> TextChannels { get; set; } = new ObservableCollection<TextChannelViewModel>();

        private readonly GuildChannelProvider _guildChannelProvider;

        public ChannelsViewModel(GuildChannelProvider guildChannelProvider)
        {
            _guildChannelProvider = guildChannelProvider;

            LoadBaseChannels();
        }

        public async void LoadBaseChannels()
        {
            var channels = await _guildChannelProvider.GetChannelsAsync();

            channels.Select(c => TextChannelViewModel.FromGuildChannel(c))
                    .ToList()
                    .ForEach(c => TextChannels.Add(c));
        }
    }
}
