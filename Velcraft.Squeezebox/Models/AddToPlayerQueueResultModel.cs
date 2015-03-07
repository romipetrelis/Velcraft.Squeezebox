using Newtonsoft.Json;

namespace Velcraft.Squeezebox.Models
{
	public class AddToPlayerQueueResultModel
	{
		[JsonProperty("count")]
		public int Count {get;set;}
	}
}
