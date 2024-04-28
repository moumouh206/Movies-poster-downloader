using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using TMDbLib.Objects.General;

namespace Movies_poster_downloader
{
    internal class SearchMovie2 : TMDbLib.Objects.Search.SearchMovieTvBase
    {
        [JsonProperty("adult")]
        public bool Adult { get; set; }

        [JsonProperty("original_title")]
        public string OriginalTitle { get; set; }

        [JsonProperty("release_date")]
        public DateTime? ReleaseDate { get; set; }

        [JsonProperty("title")]
        public string Title { get; set; }

        [JsonProperty("video")]
        public bool Video { get; set; }
        [JsonProperty("genres")]
        public Object[] Genres { get; set; }

        public SearchMovie2()
        {
            base.MediaType = MediaType.Movie;
        }
    }
}
