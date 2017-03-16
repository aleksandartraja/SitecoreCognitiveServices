﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Newtonsoft.Json;

namespace Sitecore.SharedSource.CognitiveServices.Models.Knowledge {
    public class GraphSearchRequest {
        [JsonProperty(PropertyName = "path")]
        public string Path { get; set; }
        [JsonProperty(PropertyName = "paper")]
        public AcademicPaper Paper { get; set; }
        [JsonProperty(PropertyName = "author")]
        public AcademicAuthor Author { get; set; }
    }
}