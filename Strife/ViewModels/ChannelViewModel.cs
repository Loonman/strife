using Discord_UWP.SharedModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Strife.ViewModels
{
    public class ChannelViewModel : NotificationBase
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }

        public static ChannelViewModel FromGuildChannel(GuildChannel channel)
        {
            return new ChannelViewModel
            {
                Id = channel.Id,
                Name = channel.Name,
                Type = channel.Type
            };
        }
    }
}
