using Newtonsoft.Json;
using System;
using Velcraft.Squeezebox.Models;

namespace Velcraft.Squeezebox.Converters
{
	internal class RepeatModeConverter : JsonConverter
	{
		const int Off = 0;
		const int Track = 1;
		const int Playlist = 2;

		public override void WriteJson (JsonWriter writer, object value, JsonSerializer serializer)
		{
			var m = (RepeatMode)value;
			switch (m) {
			case RepeatMode.Off:
				writer.WriteValue (Off);
				break;
			case RepeatMode.Playlist:
				writer.WriteValue (Playlist);
				break;
			case RepeatMode.Track:
				writer.WriteValue (Track);
				break;
			default:
				throw new NotSupportedException (string.Format("RepeatMode '{0}' not supported", m.ToString()));
			}
		}

		public override object ReadJson (JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
		{
			var value = Convert.ToInt32(reader.Value);

			switch (value) {
			case Off:
				return RepeatMode.Off;
			case Track:
				return RepeatMode.Track;
			case Playlist:
				return RepeatMode.Playlist;
			default: 
				throw new NotSupportedException (string.Format ("RepeatMode '{0}' not supported", value));
			}
		}

		public override bool CanConvert (Type objectType)
		{
			return objectType == typeof(RepeatMode);
		}
	}
}
