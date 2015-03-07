using Newtonsoft.Json;

namespace Velcraft.Squeezebox.Models
{
	public class GenreModel
	{
		[JsonProperty("id")]
		public string Id {get;set;}

		[JsonProperty("genre")]
		public string Name {get;set;}
	}
}
