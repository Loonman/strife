using Discord_UWP.SharedModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Strife.ViewModels
{
    public class TextChannelViewModel
    {
        public string Id { get; set; }
        public string Name { get; set; }

        public static TextChannelViewModel FromGuildChannel(GuildChannel channel)
        {
            return new TextChannelViewModel
            {
                Id = channel.Id,
                Name = channel.Name
            };
        }
    }
}
