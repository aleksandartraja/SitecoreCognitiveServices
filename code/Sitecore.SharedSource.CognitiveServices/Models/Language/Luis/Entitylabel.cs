﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Sitecore.SharedSource.CognitiveServices.Models.Language.Luis {
    public class Entitylabel {
        public string EntityName { get; set; }
        public int StartTokenIndex { get; set; }
        public int EndTokenIndex { get; set; }
    }
}