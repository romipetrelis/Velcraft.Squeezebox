using Newtonsoft.Json;
using System;

namespace Velcraft.Squeezebox.Converters
{
	internal class BoolyConverter : JsonConverter
	{
		public override object ReadJson (JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
		{
			return IntToBool (reader.Value);
		}

		public override void WriteJson (JsonWriter writer, object value, JsonSerializer serializer)
		{
			writer.WriteValue (BoolToInt(value).ToString());
		}

		public override bool CanConvert (Type objectType)
		{
			return objectType == typeof(int);
		}

		private static bool IntToBool(object value)
		{
			return value != null && value.ToString () == "1";
		}

		private static int BoolToInt(object value)
		{
			return value != null && Convert.ToBoolean (value) ? 1 : 0;
		}
	}
}
