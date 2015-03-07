namespace Velcraft.Squeezebox
{
	static public class SqueezeboxServiceFactory
	{
		static public ISqueezeboxService Create(string host, int port)
		{
			return new SqueezeboxHttpClient (string.Format("http://{0}:{1}/", host, port));
		}
	}
}
