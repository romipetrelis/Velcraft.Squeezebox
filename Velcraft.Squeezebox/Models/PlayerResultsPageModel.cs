using Newtonsoft.Json;

namespace Velcraft.Squeezebox.Models
{
	public class PlayerResultsPageModel
	{
		[JsonProperty("players_loop")]
		public PlayerModel[] Players {get;set;}
	}
}
