﻿using Sitecore.SharedSource.CognitiveServices.Models.Bing;
using Sitecore.SharedSource.CognitiveServices.Enums;
using Sitecore.SharedSource.CognitiveServices.Models.Bing.SpellCheck;

namespace Sitecore.SharedSource.CognitiveServices.Services.Bing
{
    public interface ISpellCheckService
    {
        SpellCheckResponse SpellCheck(string text, SpellCheckModeOptions mode = SpellCheckModeOptions.None, string languageCode = "");
    }
}