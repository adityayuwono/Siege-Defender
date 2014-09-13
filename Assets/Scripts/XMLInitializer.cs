using System.Collections;
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
        /// <summary>
        /// Default Engine this is a fresh game
        /// </summary>
        public TextAsset EngineText;

        /// <summary>
        /// Yeah... i don't create immortal Components, they just don't feel right
        /// </summary>
        public static string EngineXML;

        public void Start()
        {
            var loader = new WWW(FilePaths.Loading + "/Engine.xml");
            
            // Start preparing XML
            StartCoroutine(InitializeEngine(loader));
        }

        private IEnumerator InitializeEngine(WWW loader)
        {
            // Wait until it is done loading, poor thing the file is long
            while (!loader.isDone)
                yield return null;

            var isUnityEditor = false;
#if UNITY_EDITOR
            isUnityEditor=false;
#endif

            string engineText;
            if (loader.error != null || isUnityEditor)
            {
                // We don't have an XML yet, let's create one based on the template
                engineText = EngineText.text;
                using (var sw = new StreamWriter(loader.url))
                {
                    sw.Write(engineText);
                    sw.Close();
                }
            }
            else
                // Woohooo found the file
                engineText = loader.text;



            // Keep it for later, the rest of the scenes are going to need this cutie
            EngineXML = engineText;

            // Done, we simply load the next scene
            Application.LoadLevel(1);
        }
    }
}
