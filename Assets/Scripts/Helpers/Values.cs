﻿using UnityEngine;

namespace Scripts.Helpers
{
    public static class Values
    {
        public const float GUI_CIRCLE_SIZE_F = 0.4f;
        public const float GUI_CROSSHAIR_SIZE_F = 0.08f;
        public static float GUI_CROSSHAIR_HALFSIZE_F
        {
            get { return GUI_CROSSHAIR_SIZE_F/2f; }
        }

        public const int CROSSHAIR_LAYERMASK = ~(1 << 9);

        public const char DAMAGE_DELIMITER = '-';

        public static class Defaults
        {
            public const string BOSS_CHARACTER_ROOT_TAG = "Character";
            public const string WAYPOINT_TRANSFORM_TAG = "Waypoint";
            public const string WALKING_ANIMATION_BOOL_TAG = "Walk";
        }
    }

    public static class FilePaths
    {
        public static string Loading
        {
            get
            {
#if UNITY_ANDROID
                return Application.persistentDataPath;
#else
                return "file://" + Application.dataPath;
#endif
            }
        }

        public static string Saving
        {
            get
            {
#if UNITY_ANDROID
                return Application.persistentDataPath;
#else
                return Application.dataPath;
#endif
            }
        }
    }
}
