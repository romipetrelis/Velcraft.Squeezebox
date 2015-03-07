using Newtonsoft.Json;

namespace Velcraft.Squeezebox.Models
{
	public class AlbumModel
	{
		[JsonProperty("id")]
		public string Id {get;set;}

		[JsonProperty("album")]
		public string Title {get;set;}

		[JsonProperty("artist")]
		public string Artist {get;set;}

		[JsonProperty("artist_id")]
		public string ArtistId {get;set;}

		[JsonProperty("artwork_track_id")]
		public string CoverId {get;set;}
	}
}

