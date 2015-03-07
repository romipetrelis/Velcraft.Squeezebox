using System;
using Velcraft.Squeezebox.Models;

namespace Velcraft.Squeezebox.Tests
{
	static internal class StringExtensions{
		public static bool ToPowerState(this string value)
		{
			return value == "on";
		}

		public static Button ToButton(this string value)
		{
			switch (value.ToLower()) {
			case "power":
				return Button.Power;
			case "next":
				return Button.Next;
			case "previous":
				return Button.Previous;
			case "pause":
				return Button.Pause;
			case "play":
				return Button.Play;
			case "shuffle":
				return Button.Shuffle;
			case "repeat":
				return Button.Repeat;
			default:
				throw new NotSupportedException (string.Format ("Button '{0}' is not supported", value));
			}
		}

		public static PlayerMode ToPlayerMode(this string value)
		{
			switch (value.ToLower ()) {
			case "playing":
				return PlayerMode.Playing;
			case "paused":
				return PlayerMode.Paused;
			default:
				throw new NotSupportedException (string.Format ("PlayerMode '{0}' is not supported", value));
			}
		}

		public static ShuffleMode ToShuffleMode(this string value)
		{
			switch (value.ToLower ()) {
			case "all":
				return ShuffleMode.All;
			case "album":
				return ShuffleMode.Album;
			case "off":
				return ShuffleMode.Off;
			default:
				throw new NotSupportedException (string.Format ("ShuffleMode '{0}' is not supported", value));
			}
		}

		public static RepeatMode ToRepeatMode(this string value)
		{
			switch (value.ToLower ()) {
			case "off":
				return RepeatMode.Off;
			case "playlist":
				return RepeatMode.Playlist;
			case "track":
				return RepeatMode.Track;
			default:
				throw new NotSupportedException (string.Format ("RepeatMode '{0}' is not supported", value));
			}
		}
	} 
}
