using Scripts.Roots;
using UnityEngine;

namespace Scripts.Contexts
{
	/// <summary>
	///     Deserialize XML then store it
	/// </summary>
	public class DataContext : MonoBehaviour
	{
		public static DataRoot Instance { get; set; }

		public TextAsset DefaultPlayerSettings;

		private void Awake()
		{
			Instance = new DataRoot();
		}

		public void Start()
		{
			// Load Engine from external file if there's a path defined, should be for development build only
			// If no path is defined, will return the engine included with build
			var enginePath = "";
#if UNITY_EDITOR
			enginePath = "Assets/Resources/Engine.xml";
#else
			enginePath = "Engine.xml";
#endif


#if !UNITY_ANDROID
			Instance.EngineXML = LoadFile(enginePath, "" /*No default engine, because it's checked earlier*/);
#else
			Instance.EngineXML = Resources.Load<TextAsset>("Engine").text;
#endif
			UnityEngine.Debug.Log(enginePath);

			Instance.DefaultPlayerSettings = DefaultPlayerSettings; 
			// Start preparing XML
			Instance.InitializeEngine();
		}
	}
}