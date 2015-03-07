using Newtonsoft.Json;

namespace Velcraft.Squeezebox.Models
{
	public class GenreResultsPageModel
	{
		[JsonProperty("count")]
		public int Count {get;set;}

		[JsonProperty("genres_loop")]
		public GenreModel[] Genres { get; set; }
	}
}
