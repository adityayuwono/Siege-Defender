using System;
using System.IO;
using Scripts.Helpers;
using UnityEngine;

namespace Scripts
{
    /// <summary>
    /// Deserialize XML then store it
    /// </summary>
    public class XMLInitializer : MonoBehaviour
    {
        public TextAsset DefaultPlayerSettings;



        /// <summary>
        /// Text file that holds all the engine data
        /// </summary>
        public static string EngineXML;
        
        /// <summary>
        /// Text file that holds all the player's progress
        /// </summary>
        public static string PlayerSettingsXML;



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
            EngineXML = LoadFile(enginePath, "" /*No default engine, because it's checked earlier*/);
#else
            EngineXML = Resources.Load<TextAsset>("Engine").text;
#endif

            // Start preparing XML
            InitializeEngine();
        }

        private void InitializeEngine()
        {
            // Keep it for later, the rest of the scenes are going to need this cutie
            // TODO: improve to cloud saving, and encrypt
            PlayerSettingsXML = LoadFile(FilePaths.Loading + Values.Defaults.PlayerProgressFileName, DefaultPlayerSettings.text);

            // Done, we simply load the next scene
            // it clears everything we put on scene, if for example we are editing a prefab
            Application.LoadLevel(1);
        }

        private string LoadFile(string path, string defaultIfNotFound)
        {
            string file;

            try
            {
                // Try to load save file, if any
                var sr = new StreamReader(path);
                file = sr.ReadToEnd();
                sr.Close();
            }
            catch (Exception ex)
            {
                // No save file yet, just create a default one
                file = defaultIfNotFound;
            }

            return file;
        }
    }
}
