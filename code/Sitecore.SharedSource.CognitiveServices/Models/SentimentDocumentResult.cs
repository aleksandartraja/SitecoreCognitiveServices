﻿using Newtonsoft.Json;

namespace Sitecore.SharedSource.CognitiveServices.Models {
    public class SentimentDocumentResult {
        [JsonProperty("keyPhrases")]
        public string[] KeyPhrases { get; set; }

        [JsonProperty("id")]
        public string Id { get; set; }
    }
}