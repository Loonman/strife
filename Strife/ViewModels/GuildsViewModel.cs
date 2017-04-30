using Strife.Domain;
using Strife.Domain.GuildStorage;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Strife.ViewModels
{
    public class GuildsViewModel : NotificationBase
    {
        public ObservableCollection<GuildViewModel> Guilds { get; set; } = new ObservableCollection<GuildViewModel>();

        private readonly IGuildStore _guildStore;

        public GuildsViewModel(IGuildStore guildStore)
        {
            _guildStore = guildStore;

            LoadGuilds();
        }

        private async void LoadGuilds()
        {
            try
            {
                var guilds = await _guildStore.GetGuildsAsync();
                guilds.Select(g => GuildViewModel.FromUserGuild(g))
                 .ToList()
                 .ForEach(g => Guilds.Add(g));
            }
            catch (Exception e)
            {
                LoadGuilds();
            }

           
        }
    }
}
