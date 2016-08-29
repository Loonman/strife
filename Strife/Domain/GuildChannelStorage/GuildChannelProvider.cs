using Discord_UWP.SharedModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Strife.Domain.GuildChannelStorage
{
    public class GuildChannelProvider
    {
        private readonly string _guildId;
        private readonly IGuildChannelStore _guildChannelStore;

        public GuildChannelProvider(IGuildChannelStore guildChannelStore, string guildId)
        {
            _guildChannelStore = guildChannelStore;
            _guildId = guildId;
        }

        public async Task<IEnumerable<GuildChannel>> GetChannelsAsync()
        {
            return await _guildChannelStore.GetChannelsAsync(_guildId);
        }
    }
}
