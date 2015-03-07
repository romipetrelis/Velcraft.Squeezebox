using Newtonsoft.Json;

namespace Velcraft.Squeezebox.Models
{
	public class TrackModel
	{
		[JsonProperty("id")]
		public string Id {get;set;}

		[JsonProperty("title")]
		public string Title {get;set;}

		[JsonProperty("url")]
		public string Url {get;set;}

		[JsonProperty("artist_id")]
		public string ArtistId {get;set;}

		[JsonProperty("artist")]
		public string Artist {get;set;}

		[JsonProperty("album_id")]
		public string AlbumId {get;set;}

		[JsonProperty("album")]
		public string Album {get;set;}

		[JsonProperty("coverid")]
		public string CoverId {get;set;}

		[JsonProperty("genre_id")]
		public string GenreId {get;set;}

		[JsonProperty("genre_ids")]
		public string GenreIdsCsv { get; set; }

		[JsonProperty("genre")]
		public string Genre {get;set;}

		[JsonProperty("genres")]
		public string GenresCsv {get;set;}

		[JsonProperty("year")]
		public int Year {get;set;}

		[JsonProperty("duration")]
		public decimal Duration { get; set; }

		[JsonProperty("rate")]
		public int Rating { get; set; }

		[JsonProperty("tracknum")]
		public int TrackNumber {get;set;}
	}
}
