using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Discord_UWP.SharedModels;
using Discord_UWP.API.Guild;
using Discord_UWP.Gateway;

namespace Strife.Domain.GuildChannelStorage
{
    public class GuildChannelStore : IGuildChannelStore
    {
        public event EventHandler<GuildChannelCreatedEventArgs> GuildChannelCreated;
        public event EventHandler<GuildChannelEditedEventArgs> GuildChannelEdited;
        public event EventHandler<GuildChannelDeletedEventArgs> GuildChannelDelted;

        private readonly IGateway _gateway;
        private readonly IGuildService _guildService;

        public GuildChannelStore(IGuildService guildService, IGateway gateway)
        {
            _guildService = guildService;
            _gateway = gateway;
        }

        public async Task<IEnumerable<GuildChannel>> GetChannelsAsync(string guildId)
        {
            return await _guildService.GetGuildChannels(guildId);
        }
    }
}
