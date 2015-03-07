using Newtonsoft.Json;

namespace Velcraft.Squeezebox.Models
{
	public class PlayersResultModel
	{
		[JsonProperty("players_loop")]
		public PlayerModel[] Players {get;set;}
	}
}
