using NUnit.Framework;
using System;
using TechTalk.SpecFlow;
using Velcraft.Squeezebox.Models;
using System.Linq;
using System.Threading;

namespace Velcraft.Squeezebox.Tests.Steps
{
	[Binding]
	public class PlayerSteps
	{
		private readonly PlayerStepsScenarioContext _ctx;

		public PlayerSteps(PlayerStepsScenarioContext ctx)
		{
			_ctx = ctx;
		}

		[Given("a Squeezebox player (.*) managed by host (.*) on port (.*)")]
		public void GivenASqueezeboxPlayerManagedByHostOnPort(string playerId, string host, int port)
		{
			_ctx.Service = SqueezeboxServiceFactory.Create (host, port);
			_ctx.PlayerId = playerId;
		}

		[Given("that the player is powered (.*)")]
		public void GivenThatThePlayerIsPowered(string powerState)
		{
			var isOn = _ctx.Service.GetPlayerStatus (_ctx.PlayerId, 0, null).IsPowerOn;
			var desired = powerState.ToPowerState ();

			if (isOn != desired) {
				WhenIPressButton ("Power");
				ThenThePlayerIsPowered (powerState);
			}
		}

		[Given("a queued playlist")]
		public void GivenAQueuedPlaylist()
		{
			var playlist = _ctx.Service.GetPlayerStatus (_ctx.PlayerId, 5, null).Tracks;

			if (playlist == null || playlist.Length == 0) {
				//TODO: add tracks to queue
				throw new NotImplementedException ("Need to add tracks to queue");
			}
		}

		[When("I press the (.*) button")]
		public void WhenIPressButton(string buttonName)
		{
			var button = buttonName.ToButton ();

			PrePressHandler (button);

			_ctx.Service.PressPlayerButton (_ctx.PlayerId, button);

			PostPressHandler (button);
		}

		private void PrePressHandler(Button button)
		{
			var status = _ctx.Service.GetPlayerStatus (_ctx.PlayerId, 1, null);

			if (button == Button.Play || button == Button.Next || button == Button.Previous) {
				_ctx.BeforeTrackId = status.Tracks [0].Id;
			}

			if (button == Button.Shuffle) {
				_ctx.ShuffleHistory [_ctx.ShuffleIndex] = status.ShuffleMode;
			}

			if (button == Button.Repeat) {
				_ctx.RepeatHistory [_ctx.RepeatIndex] = status.RepeatMode;
			}
		}

		private void PostPressHandler(Button button)
		{
			var status = _ctx.Service.GetPlayerStatus (_ctx.PlayerId, 1, null);

			if (button == Button.Play || button == Button.Next || button == Button.Previous) {
				_ctx.AfterTrackId = status.Tracks [0].Id;
			}

			if (button == Button.Shuffle) {
				_ctx.ShuffleIndex = _ctx.ShuffleIndex == _ctx.ShuffleHistory.Length - 1 ? 0 : _ctx.ShuffleIndex + 1;
				_ctx.ShuffleHistory [_ctx.ShuffleIndex] = status.ShuffleMode;
			}

			if (button == Button.Shuffle) {
				_ctx.RepeatIndex = _ctx.RepeatIndex == _ctx.RepeatHistory.Length - 1 ? 0 : _ctx.RepeatIndex + 1;
				_ctx.RepeatHistory [_ctx.RepeatIndex] = status.RepeatMode;
			}
		}

		[When("I wait (.*) ms")]
		public void WhenIWaitMilliseconds(int timeout)
		{
			Thread.Sleep (timeout);
		}

		[When("I set the volume to (.*)")]
		public void WhenISetTheVolumeTo(int level)
		{
			_ctx.Service.SetPlayerVolume (_ctx.PlayerId, level);
		}

		[When("I set the shuffle mode to (.*)")]
		public void WhenISetTheShuffleModeTo(string shuffleMode)
		{
			_ctx.Service.SetPlayerShuffleMode (_ctx.PlayerId, shuffleMode.ToShuffleMode ());
		}

		[When("I set the repeat mode to (.*)")]
		public void WhenISetTheRepeatModeTo(string repeatMode)
		{
			_ctx.Service.SetPlayerRepeatMode (_ctx.PlayerId, repeatMode.ToRepeatMode ());
		}

		[Then("the player is powered (.*)")]
		public void ThenThePlayerIsPowered(string powerState)
		{
			var expected = powerState.ToPowerState ();

			Assert.AreEqual (expected, _ctx.Service.GetPlayerStatus (_ctx.PlayerId, 0, null).IsPowerOn);
		}

		[Then("the player mode is (.*)")]
		public void ThenThePlayerModeIs(string playerMode)
		{	
			var expected = playerMode.ToPlayerMode ();

			Assert.AreEqual (expected, _ctx.Service.GetPlayerStatus (_ctx.PlayerId, 0, null).PlayerMode);
		}

		[Then("the current track changes")]
		public void ThenTheCurrentTrackChanges()
		{
			Assert.AreNotEqual (_ctx.AfterTrackId, _ctx.BeforeTrackId);
		}

		[Then("the shuffle mode changes")]
		public void ThenTheShuffleModeChanges()
		{
			Assert.IsTrue (_ctx.ShuffleHistory.Any (o => o != null), "Each item in the shuffle history is null");

			foreach (var item in _ctx.ShuffleHistory) {
				if (item == null)
					continue;

				Assert.IsTrue(_ctx.ShuffleHistory.Count (o => o == item) == 1, 
					string.Format("There's more than one '{0}' entry in the shuffle history", (ShuffleMode)item));
			}
		}

		[Then("the repeat mode changes")]
		public void ThenTheRepeatModeChanges()
		{
			Assert.IsTrue (_ctx.RepeatHistory.Any (o => o != null), "Each item in the repeat history is null");

			foreach (var item in _ctx.RepeatHistory) {
				if (item == null)
					continue;

				Assert.IsTrue(_ctx.RepeatHistory.Count (o => o == item) == 1, 
					string.Format("There's more than one '{0}' entry in the repeat history", (RepeatMode)item));
			}
		}

		[Then("the volume is (.*)")]
		public void ThenTheVolumeIs(int level)
		{
			Assert.AreEqual (level, _ctx.Service.GetPlayerStatus (_ctx.PlayerId, 0).MixerVolume);
		}

		[Then("the shuffle mode is (.*)")]
		public void ThenTheShuffleModeIs(string shuffleMode)
		{
			var actual = _ctx.Service.GetPlayerShuffleMode (_ctx.PlayerId);

			Assert.AreEqual (shuffleMode.ToShuffleMode (), actual);
		}

		[Then("the repeat mode is (.*)")]
		public void ThenTheRepeatModeIs(string repeatMode)
		{
			var actual = _ctx.Service.GetPlayerRepeatMode (_ctx.PlayerId);

			Assert.AreEqual (repeatMode.ToRepeatMode (), actual);
		}
	}
}
