﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Sitecore.SharedSource.CognitiveServices.Models.Language.Luis {
    public class PublishRequest {
        public string VersionId { get; set; }
        public string IsStaging { get; set; }
    }
}