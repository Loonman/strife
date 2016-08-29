using Discord_UWP.SharedModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Strife.ViewModels
{
    public class GuildViewModel
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string GuildIconUrl { get; set; }

        public static GuildViewModel FromUserGuild(UserGuild guild)
        {
            return new GuildViewModel
            {
                Id = guild.Id,
                Name = guild.Name,
                GuildIconUrl = $"https://cdn.discordapp.com/icons/{guild.Id}/{guild.Icon}.jpg"
            };
        }

        public static GuildViewModel FromGuildChannel(Guild guild)
        {
            return new GuildViewModel
            {
                Id = guild.Id,
                Name = guild.Name,
                GuildIconUrl = $"https://cdn.discordapp.com/icons/{guild.Id}/{guild.Icon}.jpg"
            };
        }
    }
}
