﻿using System;
using Microsoft.Extensions.DependencyInjection;
using Sitecore.SharedSource.CognitiveServices.ImageSearch.Models.Utility;
using Microsoft.SharedSource.CognitiveServices.Models.Vision.Computer;

namespace Sitecore.SharedSource.CognitiveServices.ImageSearch.Factories
{
    public class ImageDescriptionFactory : IImageDescriptionFactory
    {
        protected readonly IServiceProvider Provider;

        public ImageDescriptionFactory(IServiceProvider provider)
        {
            Provider = provider;
        }

        public virtual IImageDescription Create()
        {
            var obj = Provider.GetService<IImageDescription>();

            obj.CognitiveDescription = new Description();
            obj.AltDescription = string.Empty;
            obj.ItemId = string.Empty;
            obj.Database = string.Empty;
            obj.Language = string.Empty;

            return obj;
        }

        public virtual IImageDescription Create(Description cognitiveDescription, string altDescription, string itemId, string database, string language)
        {
            var obj = Create();

            obj.CognitiveDescription = cognitiveDescription;
            obj.AltDescription = altDescription;
            obj.ItemId = itemId;
            obj.Database = database;
            obj.Language = language;

            return obj;
        }
    }
}