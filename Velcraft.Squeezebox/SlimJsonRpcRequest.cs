using Newtonsoft.Json;
using System.Collections.Generic;

namespace Velcraft.Squeezebox
{
	internal class SlimJsonRpcRequest
	{
		private readonly List<object> _parameters;

		public SlimJsonRpcRequest(string playerIdentifier, object[] args)
		{
			_parameters = new List<object>();
			_parameters.Add(playerIdentifier);
			_parameters.Add(args);
		}

		[JsonProperty(PropertyName="method")]
		public string Method { get { return "slim.request"; } }

		[JsonProperty(PropertyName="id")]
		public int Id {get { return 1; }}

		[JsonProperty(PropertyName="params")]
		public IEnumerable<object> Parameters { get { return _parameters; } }
	}
}

