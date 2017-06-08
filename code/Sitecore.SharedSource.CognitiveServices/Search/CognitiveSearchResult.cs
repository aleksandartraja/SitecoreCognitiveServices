﻿using System.Collections.Generic;
using System.Web.Script.Serialization;
using Microsoft.ProjectOxford.Emotion.Contract;
using Microsoft.ProjectOxford.Text.Sentiment;
using Microsoft.ProjectOxford.Vision.Contract;
using Sitecore.ContentSearch;
using Sitecore.ContentSearch.SearchTypes;
using Sitecore.SharedSource.CognitiveServices.Models.Analysis;
using Face = Microsoft.ProjectOxford.Face.Contract.Face;

namespace Sitecore.SharedSource.CognitiveServices.Search
{
    public class CognitiveSearchResult : SearchResultItem, ICognitiveSearchResult
    {
        #region properties
        
        [IndexField("EmotionAnalysis")]
        public string EmotionAnalysisValue { get; set; }
        
        public Emotion[] EmotionAnalysis => SaturateValue<Emotion[]>(EmotionAnalysisValue) ?? new Emotion[0];
        
        [IndexField("FacialAnalysis")]
        public string FacialAnalysisValue { get; set; }

        public Face[] FacialAnalysis => SaturateValue<Face[]>(FacialAnalysisValue) ?? new Face[0];
        
        [IndexField("TextAnalysis")]
        public string TextAnalysisValue { get; set; }

        public OcrResults TextAnalysis => SaturateValue<OcrResults>(TextAnalysisValue) ?? new OcrResults();
        
        [IndexField("VisionAnalysis")]
        public string VisionAnalysisValue { get; set; }

        public AnalysisResult VisionAnalysis => SaturateValue<AnalysisResult>(VisionAnalysisValue) ?? new AnalysisResult();
        
        [IndexField("LinkAnalysis")]
        public string LinkAnalysisValue { get; set; }

        public List<LinkAnalysisResult> LinkAnalysis => SaturateValue<List<LinkAnalysisResult>>(LinkAnalysisValue) ?? new List<LinkAnalysisResult>();
        
        [IndexField("SentimentAnalysis")]
        public string SentimentAnalysisValue { get; set; }

        public SentimentResponse SentimentAnalysis => SaturateValue<SentimentResponse>(SentimentAnalysisValue) ?? new SentimentResponse();

        [IndexField("KeyPhraseAnalysis")]
        public string KeyPhraseAnalysisValue { get; set; }

        public List<KeyPhraseAnalysisResult> KeyPhraseAnalysis => SaturateValue<List<KeyPhraseAnalysisResult>>(KeyPhraseAnalysisValue) ?? new List<KeyPhraseAnalysisResult>();

        [IndexField("LinguisticAnalysis")]
        public string LinguisticAnalysisValue { get; set; }

        public List<LinguisticAnalysisResult> LinguisticAnalysis => SaturateValue<List<LinguisticAnalysisResult>>(LinguisticAnalysisValue) ?? new List<LinguisticAnalysisResult>();

        [IndexField("_uniqueid")]
        public string UniqueId { get; set; }
        
        #endregion properties

        private static T SaturateValue<T>(string value)
        {
            if (string.IsNullOrEmpty(value))
                return default(T);

            try
            {
                return new JavaScriptSerializer().Deserialize<T>(value);
            }
            catch { }

            return default(T);
        }
    }
}