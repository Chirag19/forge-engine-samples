﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bimangle.ForgeEngine.Common.Formats.Svf.Revit;
using Bimangle.ForgeEngine.Revit.Core;

namespace Bimangle.ForgeEngine.Revit.Config
{
    [Serializable]
    class AppConfigSvf
    {
        public string LastTargetPath { get; set; }
        public bool AutoOpenAllow { get; set; }
        public string AutoOpenAppName { get; set; }
        public string VisualStyle { get; set; }
        public int LevelOfDetail { get; set; }
        public List<FeatureType> Features { get; set; }

        public AppConfigSvf()
        {
            LastTargetPath = string.Empty;
            AutoOpenAllow = true;
            AutoOpenAppName = null;
            VisualStyle = null;
            LevelOfDetail = -1;
            Features = new List<FeatureType>
            {
                FeatureType.GenerateThumbnail,
                FeatureType.GenerateModelsDb,
                FeatureType.Export2DViewOnlySheet,
                FeatureType.UseBasicRenderColor
            };
        }

        public AppConfigSvf Clone()
        {
            return new AppConfigSvf
            {
                LastTargetPath = LastTargetPath,
                AutoOpenAllow = AutoOpenAllow,
                AutoOpenAppName = AutoOpenAppName,
                VisualStyle = VisualStyle,
                LevelOfDetail = LevelOfDetail,
                Features = Features?.ToList() ?? new List<FeatureType>()
            };
        }
    }
}
