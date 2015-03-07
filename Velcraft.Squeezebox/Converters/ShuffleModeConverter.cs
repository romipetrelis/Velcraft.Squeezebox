using Newtonsoft.Json;
using System;
using Velcraft.Squeezebox.Models;

namespace Velcraft.Squeezebox.Converters
{
	internal class ShuffleModeConverter : JsonConverter
	{
		const int Off = 0;
		const int Album = 2;
		const int All = 1;

		public override void WriteJson (JsonWriter writer, object value, JsonSerializer serializer)
		{
			var m = (ShuffleMode)value;
			switch (m) {
			case ShuffleMode.Off:
				writer.WriteValue (Off);
				break;
			case ShuffleMode.Album:
				writer.WriteValue (Album);
				break;
			case ShuffleMode.All:
				writer.WriteValue (All);
				break;
			default:
				throw new NotSupportedException (string.Format("ShuffleMode '{0}' not supported", m.ToString()));
			}
		}

		public override object ReadJson (JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
		{
			var value = Convert.ToInt32(reader.Value);

			switch (value) {
			case Off:
				return ShuffleMode.Off;
			case Album:
				return ShuffleMode.Album;
			case All:
				return ShuffleMode.All;
			default: 
				throw new NotSupportedException (string.Format ("ShuffleMode '{0}' not supported", value));
			}
		}

		public override bool CanConvert (Type objectType)
		{
			return objectType == typeof(ShuffleMode);
		}
	}
}

