using UnityEngine;

namespace Scripts.Helpers
{
	public static class FilePaths
	{
		public static string Loading
		{
			get
			{
#if UNITY_ANDROID
				return Application.persistentDataPath + "/";
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
				return Application.persistentDataPath + "/";
#elif UNITY_STANDALONE_WIN
                return "";
#else
                return Application.dataPath+"/";
#endif
			}
		}
	}
}