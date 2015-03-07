using Newtonsoft.Json;

namespace Velcraft.Squeezebox.Models
{
	public class ArtistResultsPageModel
	{
		[JsonProperty("count")]
		public int Count { get; set; }

		[JsonProperty("artists_loop")]
		public ArtistModel[] Artists { get; set; }
	}
}
