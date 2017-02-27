﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Sitecore.SharedSource.CognitiveServices.Models.Academic {
    public class CalcHistogramResponse {
        public string Expr { get; set; }
        public int Num_Entities { get; set; }
        public List<HistogramResult> Histograms { get; set; } 
    }
}