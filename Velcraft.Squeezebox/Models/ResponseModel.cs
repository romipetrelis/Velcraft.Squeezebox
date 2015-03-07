using Newtonsoft.Json;

namespace Velcraft.Squeezebox.Models
{
	internal class ResponseModel<T>
	{
		[JsonProperty("result")]
		public T Result {get;set;}
	}
}
