using Discord_UWP.SharedModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Strife.Domain.GuildChannelStorage
{
    public class GuildChannelCreatedEventArgs : EventArgs
    {
        public GuildChannel GuildChannel { get; set; }
    }

    public class GuildChannelEditedEventArgs : EventArgs
    {
        public GuildChannel GuildChannel { get; set; }
    }

    public class GuildChannelDeletedEventArgs : EventArgs
    {
        public string GuildId { get; set; }
        public string ChannelId { get; set; }
    }


    public interface IGuildChannelStore
    {
        event EventHandler<GuildChannelCreatedEventArgs> GuildChannelCreated;
        event EventHandler<GuildChannelEditedEventArgs> GuildChannelEdited;
        event EventHandler<GuildChannelDeletedEventArgs> GuildChannelDelted;

        Task<IEnumerable<GuildChannel>> GetChannelsAsync(string guildId);
    }
}
