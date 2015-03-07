using Newtonsoft.Json;

namespace Velcraft.Squeezebox.Models
{
	public class PlayerModel
	{
		[JsonProperty("playerid")]
		public string Id {get;set;}

		[JsonProperty("name")]
		public string Name {get;set;}
	}
}
