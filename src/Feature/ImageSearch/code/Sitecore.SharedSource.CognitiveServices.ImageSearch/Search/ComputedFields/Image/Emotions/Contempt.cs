﻿using System.Linq;

namespace Sitecore.SharedSource.CognitiveServices.ImageSearch.Search.ComputedFields.Image.Emotions
{
    public class Contempt : BaseComputedField
    {
        protected override object GetFieldValue(CognitiveIndexableImageItem cognitiveIndexable)
        {
            return (cognitiveIndexable.Emotions != null && cognitiveIndexable.Emotions.Length > 0)
                ? (object)cognitiveIndexable.Emotions?.Average(x => x.Scores.Contempt)
                : null;
        }
    }
}