using System;
using System.Net.Http;
using Newtonsoft.Json;
using System.Collections.Generic;
using Velcraft.Squeezebox.Models;
using Newtonsoft.Json.Linq;

namespace Velcraft.Squeezebox
{
	internal class SqueezeboxHttpClient : ISqueezeboxService
	{
		private readonly HttpClient _httpClient;
		private readonly string _baseUrl;

		public SqueezeboxHttpClient (string baseUrl)
		{
			_httpClient = new HttpClient ();
			_baseUrl = baseUrl;
		}

		public PlayerStatusModel GetPlayerStatus(string playerId, int tracksToRetrieve, string[] tags=null)
		{
			var startWithCurrentTrack = tracksToRetrieve > 0 ? "-" : "0";

			var args = new List<object>{ "status", startWithCurrentTrack, tracksToRetrieve };

			if (tags != null && tags.Length > 0) {
				args.Add (string.Format ("tags:{0}", string.Concat (tags)));
			}

			var request = new SlimJsonRpcRequest (playerId, args.ToArray());
			var json = PostJsonRpcRequest (request);

			return JsonConvert.DeserializeObject<ResponseModel<PlayerStatusModel>> (json).Result;
		}

		public PlayersResultModel GetPlayers(int offset, int limit)
		{
			var request = new SlimJsonRpcRequest (string.Empty, new object[]{ "players", offset, limit });
			var json = PostJsonRpcRequest (request);

			return JsonConvert.DeserializeObject<ResponseModel<PlayersResultModel>> (json).Result;
		}

		public void PressPlayerButton(string playerId, Button button)
		{
			object request;

			switch (button) {
			case Button.Next:
				request = new SlimJsonRpcRequest (playerId, new []{ "playlist", "index", "+1" });
				break;
			case Button.Pause:
				request = new SlimJsonRpcRequest (playerId, new[]{ "pause" });
				break;
			case Button.Play:
				request = new SlimJsonRpcRequest (playerId, new[]{ "play" });
				break;
			case Button.Power:
				request = new SlimJsonRpcRequest (playerId, new []{ "power" });
				break;
			case Button.Previous:
				request = new SlimJsonRpcRequest (playerId, new []{ "playlist", "index", "-1" });
				break;
			case Button.Shuffle:
				request = new SlimJsonRpcRequest (playerId, new[] { "playlist", "shuffle" });
				break;
			case Button.Repeat:
				request = new SlimJsonRpcRequest (playerId, new[] { "playlist", "repeat" });
				break;
			default:
				throw new NotSupportedException (
					string.Format("Pressing of the '{0}' player button is not yet supported.", Enum.GetName(typeof(Button), button)));
			}

			PostJsonRpcRequest (request);
		}

		public void SetPlayerVolume(string playerId, int level)
		{
			level = level > 100 ? 100 : (level < 0 ? 0 : level);

			var request = new SlimJsonRpcRequest(playerId, new object[]{"mixer", "volume", level});
			PostJsonRpcRequest (request);
		}

		public ShuffleMode GetPlayerShuffleMode (string playerId)
		{
			var request = new SlimJsonRpcRequest (playerId, new object[] { "playlist", "shuffle", "?" });
			dynamic response = JToken.Parse (PostJsonRpcRequest (request));
			var o = response.result._shuffle;
			var value = JsonConvert.SerializeObject (o);

			return JsonConvert.DeserializeObject<ShuffleMode> (value, new Converters.ShuffleModeConverter ());
		}

		public void SetPlayerShuffleMode (string playerId, ShuffleMode shuffleMode)
		{
			var value = JsonConvert.SerializeObject (shuffleMode, new Converters.ShuffleModeConverter ());

			var request = new SlimJsonRpcRequest (playerId, new object[] { "playlist", "shuffle", value });
			PostJsonRpcRequest (request);
		}

		public RepeatMode GetPlayerRepeatMode (string playerId)
		{
			var request = new SlimJsonRpcRequest (playerId, new object[] { "playlist", "repeat", "?" });
			dynamic response = JToken.Parse (PostJsonRpcRequest (request));
			var o = response.result._repeat;
			var value = JsonConvert.SerializeObject (o);

			return JsonConvert.DeserializeObject<RepeatMode> (value, new Converters.RepeatModeConverter ());
		}

		public void SetPlayerRepeatMode (string playerId, RepeatMode repeatMode)
		{
			var value = JsonConvert.SerializeObject (repeatMode, new Converters.RepeatModeConverter ());
			var request = new SlimJsonRpcRequest (playerId, new object[] { "playlist", "repeat", value });
			PostJsonRpcRequest (request);
		}

		public AddToPlayerQueueResultModel AddToPlayerQueue (string playerId, string genreId=null, string artistId=null, string albumId=null, string trackId=null, string playlistId=null)
		{
			var args = new List<object> {"playlistcontrol", "cmd:add" };

			if (!string.IsNullOrWhiteSpace (genreId)) {
				args.Add (string.Concat ("genre_id:", genreId));
			}

			if (!string.IsNullOrWhiteSpace (artistId)) {
				args.Add (string.Concat ("artist_id:", artistId));
			}

			if (!string.IsNullOrWhiteSpace (albumId)) {
				args.Add (string.Concat ("album_id:", albumId));
			}

			if (!string.IsNullOrWhiteSpace (trackId)) {
				args.Add (string.Concat ("track_id:", trackId));
			}

			if (!string.IsNullOrWhiteSpace (playlistId)) {
				args.Add (string.Concat ("playlist_id:", playlistId));
			}

			var request = new SlimJsonRpcRequest (playerId, args.ToArray());
			var json = PostJsonRpcRequest (request);
			return JsonConvert.DeserializeObject<ResponseModel<AddToPlayerQueueResultModel>> (json).Result;
		}


		public GenreResultsPageModel GetGenres(int offset, int limit)
		{
			var request = new SlimJsonRpcRequest (string.Empty, new object[] { "genres", offset, limit });
			var json = PostJsonRpcRequest (request);
			return JsonConvert.DeserializeObject<ResponseModel<GenreResultsPageModel>> (json).Result;
		}

		public ArtistResultsPageModel GetArtists(int offset, int limit, string genreId = null)
		{
			var args = new List<object> { "artists", offset, limit };

			if (!string.IsNullOrWhiteSpace (genreId)) {
				args.Add (string.Concat ("genre_id:", genreId));
			}

			var request = new SlimJsonRpcRequest (string.Empty, args.ToArray());
			var json = PostJsonRpcRequest (request);
			return JsonConvert.DeserializeObject<ResponseModel<ArtistResultsPageModel>> (json).Result;
		}

		public AlbumResultsPageModel GetAlbums (int offset, int limit, AlbumSort sort = AlbumSort.Artflow, 
			string[] tags=null, string search=null, string artistId = null, string genreId = null, string trackId = null, string albumId = null)
		{
			var args = new List<object>{ "albums", offset, limit, "sort:" + Enum.GetName(typeof(AlbumSort), sort).ToString().ToLower() };

			if (!string.IsNullOrWhiteSpace (search)) {
				//TODO: what about special characters and spaces?
				args.Add ("search:" + search);
			}

			if (!string.IsNullOrWhiteSpace (artistId)) {
				args.Add ("artist_id:" + artistId);
			}

			if (!string.IsNullOrWhiteSpace (trackId)) {
				args.Add ("track_id:" + trackId);
			}

			if (!string.IsNullOrWhiteSpace (albumId)) {
				args.Add ("album_id:" + albumId);
			}

			if (!string.IsNullOrWhiteSpace (genreId)) {
				args.Add ("genre_id:" + genreId);
			}

			if (tags != null && tags.Length > 0) {
				args.Add (string.Concat("tags:", string.Concat(tags)));
			}

			var request = new SlimJsonRpcRequest (string.Empty, args.ToArray());
			var json = PostJsonRpcRequest (request);
			return JsonConvert.DeserializeObject<ResponseModel<AlbumResultsPageModel>> (json).Result;
		}

		public TrackResultsPageModel GetTracks (int offset, int limit, string[] tags=null, string artistId = null, string genreId = null, string albumId = null, string trackId = null)
		{
			var args = new List<object>{ "tracks", offset, limit };

			if (!string.IsNullOrWhiteSpace (artistId)) {
				args.Add ("artist_id:" + artistId);
			}

			if (!string.IsNullOrWhiteSpace (trackId)) {
				args.Add ("track_id:" + trackId);
			}

			if (!string.IsNullOrWhiteSpace (albumId)) {
				args.Add ("album_id:" + albumId);
			}

			if (!string.IsNullOrWhiteSpace (genreId)) {
				args.Add ("genre_id:" + genreId);
			}

			if (tags != null && tags.Length > 0) {
				args.Add (string.Format ("tags:{0}", string.Concat (tags)));
			}
			throw new NotImplementedException ("GetTracks is not implemented yet");
		}

		public string PostAdHocRequest(string playerId, object[] args)
		{
			var request = new SlimJsonRpcRequest (playerId, args);
			return PostJsonRpcRequest (request);
		}

		private string PostJsonRpcRequest(object request)
		{
			var url = string.Concat (_baseUrl, "jsonrpc.js");

			var json = JsonConvert.SerializeObject(request);
			var response = _httpClient.PostAsync(url, new StringContent(json)).Result;
			return response.Content.ReadAsStringAsync().Result;
		}
	}
}
