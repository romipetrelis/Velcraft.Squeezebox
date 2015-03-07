using Newtonsoft.Json;

namespace Velcraft.Squeezebox.Models
{
	public class AlbumResultsPageModel
	{
		[JsonProperty("count")]
		public int Count {get;set;}

		[JsonProperty("albums_loop")]
		public AlbumModel[] Albums {get;set;}
	}
}

