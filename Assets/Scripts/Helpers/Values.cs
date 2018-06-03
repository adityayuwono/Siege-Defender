using UnityEngine;

namespace Scripts.Helpers
{
    public static class Values
    {
        public const float GuiCrosshairSizeF = 0.08f;
        public static float GuiCrosshairHalfsizeF
        {
            get { return GuiCrosshairSizeF/2f; }
        }

        public const int CrosshairLayermask = ~(1 << 9);

        public const char DamageDelimiter = '-';

        public static class Defaults
        {
            public const string PlayerProgressFileName = "PlayerSettings.xml";

            public const string BossCharacterRootName = "Character";
            public const string WaypointTransformTag = "Waypoint";
        }
    }

    public static class FilePaths
    {
        public static string Loading
        {
            get
            {
#if UNITY_ANDROID
                return Application.persistentDataPath+"/";
#elif UNITY_STANDALONE_WIN
                return "";
#else
                return "file://" + Application.dataPath + "/"; ;
#endif
            }
        }

        public static string Saving
        {
            get
            {
#if UNITY_ANDROID
                return Application.persistentDataPath+"/";
#elif UNITY_STANDALONE_WIN
                return "";
#else
                return Application.dataPath+"/";
#endif
            }
        }
    }
}
