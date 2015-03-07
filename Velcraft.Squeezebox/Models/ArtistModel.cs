using Newtonsoft.Json;

namespace Velcraft.Squeezebox.Models
{
	public class ArtistModel
	{
		[JsonProperty("id")]
		public string Id {get;set;}

		[JsonProperty("artist")]
		public string Name {get;set;}
	}
}
