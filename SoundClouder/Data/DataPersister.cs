namespace SoundClouder.Data
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using SoundClouder.Models;
    using SoundClouder.ViewModels;

    public class DataPersister
    {
        private const string ClientId = "bc9fb45ddcbe6b81c589d6e589b15087";

        private const string ClientSecret = "facb1e72b2b65eb8c0e053ea20aaa298";

        private const string BaseUrl = "https://api.soundcloud.com/{0}&client_id=bc9fb45ddcbe6b81c589d6e589b15087";

        public static async Task<IEnumerable<TrackViewModel>> GetTrack(string queryString)
        {
            if (string.IsNullOrEmpty(queryString))
            {
                return null;
            }

            string searchUrl = "tracks.json?q=" + queryString;
            
            var tracks = await HttpRequester.GetAsync<IEnumerable<Track>>(string.Format("https://api.soundcloud.com/{0}&client_id=bc9fb45ddcbe6b81c589d6e589b15087", searchUrl), null);

            if (tracks.Count<Track>() < 1)
            {
                return null;
            }

            return tracks.AsQueryable().Select(trackModel => new TrackViewModel()
            {
                Title = trackModel.Title,
                StreamUrl = trackModel.StreamUrl,
            });
        }
    }
}