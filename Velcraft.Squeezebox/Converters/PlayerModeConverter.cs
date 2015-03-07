using Newtonsoft.Json;
using System;
using Velcraft.Squeezebox.Models;

namespace Velcraft.Squeezebox.Converters
{
	internal class PlayerModeConverter : JsonConverter
	{
		const string Playing = "play";
		const string Paused = "pause";
		const string Stopped = "stop";

		public override void WriteJson (JsonWriter writer, object value, JsonSerializer serializer)
		{
			var m = (PlayerMode)value;
			switch (m) {
			case PlayerMode.Paused:
				writer.WriteValue (Paused);
				break;
			case PlayerMode.Playing:
				writer.WriteValue (Playing);
				break;
			case PlayerMode.Stopped:
				writer.WriteValue (Stopped);
				break;
			default:
				throw new NotSupportedException (string.Format("PlayerMode '{0}' not supported", m.ToString()));
			}
		}

		public override object ReadJson (JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
		{
			var value = reader.Value.ToString ();

			switch (value) {
			case Playing:
				return PlayerMode.Playing;
			case Paused:
				return PlayerMode.Paused;
			case Stopped:
				return PlayerMode.Stopped;
			default: 
				throw new NotSupportedException (string.Format ("PlayerMode '{0}' not supported", value));
			}
		}

		public override bool CanConvert (Type objectType)
		{
			return objectType == typeof(PlayerMode);
		}
	}
}

