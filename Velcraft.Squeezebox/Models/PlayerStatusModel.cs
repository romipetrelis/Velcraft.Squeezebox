using Newtonsoft.Json;
using Velcraft.Squeezebox.Converters;

namespace Velcraft.Squeezebox.Models
{
	public class PlayerStatusModel
	{
		[JsonProperty("can_seek")]
		[JsonConverter(typeof(BoolyConverter))]
		public bool CanSeek {get;set;}

		[JsonProperty("duration")]
		public decimal CurrentTrackDuration {get;set;}

		[JsonProperty("time")]
		public decimal CurrentTrackTime {get;set;}

		[JsonProperty("mixer volume")]
		public int MixerVolume {get;set;}

		[JsonProperty("mode")]
		[JsonConverter(typeof(PlayerModeConverter))]
		public PlayerMode PlayerMode {get;set;}

		[JsonProperty("player_ip")]
		public string PlayerIp {get;set;}

		[JsonProperty("player_name")]
		public string PlayerName { get; set; }

		[JsonProperty("playlist shuffle")]
		[JsonConverter(typeof(ShuffleModeConverter))]
		public ShuffleMode ShuffleMode {get;set;}

		[JsonProperty("playlist repeat")]
		[JsonConverter(typeof(RepeatModeConverter))]
		public RepeatMode RepeatMode {get;set;}

		[JsonProperty("power")]
		[JsonConverter(typeof(BoolyConverter))]
		public bool IsPowerOn {get;set;}

		[JsonProperty("playlist_loop")]
		public TrackModel[] Tracks {get;set;}
	}
}
