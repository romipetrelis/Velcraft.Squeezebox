using NUnit.Framework;
using Newtonsoft.Json;
using Velcraft.Squeezebox.Models;

namespace Velcraft.Squeezebox.Tests
{
	[TestFixture]
	public class ConverterTests
	{
		[Test]
		public void PlayerStatusResult_Enums_And_Bools_Serialize_Ok()
		{
			var sourceModel = new PlayerStatusModel{ 
				CanSeek = true, 
				RepeatMode = RepeatMode.Playlist, 
				ShuffleMode = ShuffleMode.All, 
				PlayerMode= PlayerMode.Playing 
			};
			var serialized = JsonConvert.SerializeObject (sourceModel);
			var deserialized = JsonConvert.DeserializeObject<PlayerStatusModel> (serialized);

			Assert.IsTrue (deserialized.CanSeek);
			Assert.IsTrue (deserialized.RepeatMode == RepeatMode.Playlist);
			Assert.IsTrue (deserialized.ShuffleMode == ShuffleMode.All);
			Assert.IsTrue (deserialized.PlayerMode == PlayerMode.Playing);
		}
	}
}
