﻿using System;
using System.Text;
using Microsoft.ProjectOxford.Text.Core;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Microsoft.SharedSource.CognitiveServices.Enums;
using Microsoft.SharedSource.CognitiveServices.Models.Bing;
using Microsoft.SharedSource.CognitiveServices.Models.Bing.NewsSearch;

namespace Microsoft.SharedSource.CognitiveServices.Repositories.Bing {
    public class NewsSearchRepository : TextClient, INewsSearchRepository {

        public static readonly string categoryUrl = "https://api.cognitive.microsoft.com/bing/v5.0/news/";
        public static readonly string trendingUrl = "https://api.cognitive.microsoft.com/bing/v5.0/news/trendingtopics";
        public static readonly string newsUrl = "https://api.cognitive.microsoft.com/bing/v5.0/news/search";

        public NewsSearchRepository(
            IApiKeys apiKeys)
            : base(apiKeys.BingSearch)
        {
        }

        #region Category Search

        public virtual NewsSearchCategoryResponse CategorySearch(NewsCategoryOptions category) {
            return Task.Run(async () => await CategorySearchAsync(category)).Result;
        }

        public virtual async Task<NewsSearchCategoryResponse> CategorySearchAsync(NewsCategoryOptions category)
        {
            var catName = Enum.GetName(typeof (NewsCategoryOptions), category).Replace("USUK", "US/UK");

            var response = await SendGetAsync($"{categoryUrl}?Category={catName}");

            return JsonConvert.DeserializeObject<NewsSearchCategoryResponse>(response);
        }

        #endregion Category Search

        #region Trending Search

        public virtual NewsSearchTrendResponse TrendingSearch() {
            return Task.Run(async () => await TrendingSearchAsync()).Result;
        }

        public virtual async Task<NewsSearchTrendResponse> TrendingSearchAsync() {
            var response = await SendGetAsync(trendingUrl);

            return JsonConvert.DeserializeObject<NewsSearchTrendResponse>(response);
        }

        #endregion Trending Search

        #region News Search

        public virtual NewsSearchResponse NewsSearch(string text, int countOffset = 0, string languageCode = "", SafeSearchOptions safeSearch = SafeSearchOptions.Off) {
            return Task.Run(async () => await NewsSearchAsync(text, countOffset, languageCode, safeSearch)).Result;
        }

        public virtual async Task<NewsSearchResponse> NewsSearchAsync(string text, int countOffset = 0, string languageCode = "", SafeSearchOptions safeSearch = SafeSearchOptions.Off) {

            StringBuilder sb = new StringBuilder();

            if (countOffset > 0)
                sb.Append($"?countoffset={countOffset}");

            if (!string.IsNullOrEmpty(languageCode)) {
                var concat = (sb.Length > 0) ? "?" : "&";
                sb.Append($"{concat}mkt={languageCode}");
            }

            if (safeSearch != SafeSearchOptions.Off) {
                var concat = (sb.Length > 0) ? "?" : "&";
                sb.Append($"{concat}safeSearch={Enum.GetName(typeof(SafeSearchOptions), safeSearch)}");
            }

            var response = await SendGetAsync($"{newsUrl}?q={text}{sb}");
            
            return JsonConvert.DeserializeObject<NewsSearchResponse>(response);
        }

        #endregion News Search
    }
}