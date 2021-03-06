﻿using UnityEngine;

namespace Scripts.Settings
{
    public interface ISettings
    {
        int MapSectionSize { get; }
        int ActiveAreaSize { get; }
        IntVector2 InitializePosition { get; }
        string MapsResourcesLocation { get; }
        string UnitsResourcesLocation { get; }
    }
}