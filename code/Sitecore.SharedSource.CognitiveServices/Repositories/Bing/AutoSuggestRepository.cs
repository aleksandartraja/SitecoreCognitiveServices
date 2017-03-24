﻿using System.Threading.Tasks;
using Microsoft.ProjectOxford.Text.Core;
using Newtonsoft.Json;
using Sitecore.SharedSource.CognitiveServices.Models.Bing;
using Sitecore.SharedSource.CognitiveServices.Models.Bing.AutoSuggest;

namespace Sitecore.SharedSource.CognitiveServices.Repositories.Bing
{
    public class AutoSuggestRepository : TextClient, IAutoSuggestRepository
    {
        public static readonly string suggestUrl = "https://api.cognitive.microsoft.com/bing/v5.0/suggestions/";

        public AutoSuggestRepository(
            IApiKeys apiKeys)
            : base(apiKeys.BingAutosuggest)
        {
        }

        public virtual AutoSuggestResponse GetSuggestions(string text)
        {
            return Task.Run(async () => await GetSuggestionsAsync(text)).Result;
        }

        public virtual async Task<AutoSuggestResponse> GetSuggestionsAsync(string text)
        {
            var response = await this.SendGetAsync($"{suggestUrl}?q={text}");

            return JsonConvert.DeserializeObject<AutoSuggestResponse>(response);
        }
    }
}