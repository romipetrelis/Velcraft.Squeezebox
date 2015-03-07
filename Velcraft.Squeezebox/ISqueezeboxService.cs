using Velcraft.Squeezebox.Models;

namespace Velcraft.Squeezebox
{
	public interface ISqueezeboxService
	{
		PlayerStatusModel GetPlayerStatus(string playerId, int tracksToRetrieve, string[] tags=null);

		void PressPlayerButton (string playerId, Button button);

		void SetPlayerVolume (string playerId, int level);

		ShuffleMode GetPlayerShuffleMode (string playerId);

		void SetPlayerShuffleMode (string playerId, ShuffleMode shuffleMode);

		RepeatMode GetPlayerRepeatMode (string playerId);

		void SetPlayerRepeatMode (string playerId, RepeatMode repeatMode);

		AddToPlayerQueueResultModel AddToPlayerQueue (string playerId, string genreId=null, string artistId=null, string albumId=null, string trackId=null, string playlistId=null);

		PlayerResultsPageModel GetPlayers (int offset, int limit);

		GenreResultsPageModel GetGenres(int offset, int limit);

		ArtistResultsPageModel GetArtists(int offset, int limit, string genreId=null);

		AlbumResultsPageModel GetAlbums (int offset, int limit, AlbumSort sort = AlbumSort.Artflow, string[] tags=null, string search=null, string artistId = null, string genreId = null, string trackId = null, string albumId = null);

		TrackResultsPageModel GetTracks (int offset, int limit, string[] tags=null, string artistId = null, string genreId = null, string albumId = null, string trackId = null);

		string PostAdHocRequest(string playerId, object[] args);
	}
}
