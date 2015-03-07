using Newtonsoft.Json;

namespace Velcraft.Squeezebox.Models
{
	public class TrackResultsPageModel
	{
		[JsonProperty("count")]
		public int Count{get;set;}
	}
}

