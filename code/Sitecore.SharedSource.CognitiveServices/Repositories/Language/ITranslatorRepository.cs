﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using Sitecore.SharedSource.CognitiveServices.Enums;
using Sitecore.SharedSource.CognitiveServices.Models.Language.Translator;

namespace Sitecore.SharedSource.CognitiveServices.Repositories.Language {
    public interface ITranslatorRepository
    {
        Task<GetLanguageResponse> GetLanguagesAsync(IEnumerable<TranslateScopeOptions> scopes = null);
        Task<TranslateResponse> TranslateAsync(
            string from, 
            string to,
            Stream stream,
            TranslateFormatOption format = TranslateFormatOption.wav, 
            TranslateProfanityMarkerOption marker = TranslateProfanityMarkerOption.asterisk, 
            TranslateProfanityActionOption action = TranslateProfanityActionOption.marked, 
            string voice = "", 
            IEnumerable<TranslateFeatureOptions> features = null);
    }
}