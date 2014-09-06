using System.Collections;
using System.IO;
using UnityEngine;

namespace Scripts
{
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
            var filePath = Application.persistentDataPath;
            var loader = new WWW(filePath + "/Engine.xml");
            
            // Start preparing XML
            StartCoroutine(InitializeEngine(loader));
        }

        private IEnumerator InitializeEngine(WWW loader)
        {
            // Wait until it is done loading, poor thing the file is long
            while (!loader.isDone)
                yield return null;

            string engineText;
            if (loader.error != null)
            {
                // We don't have an XML yet, let's create one based on the template
                engineText = EngineText.text;
                using (var sw = new StreamWriter(loader.url))
                    sw.Write(engineText);
            }
            else
                // Woohooo found the file
                engineText = loader.text;

#if UNITY_EDITOR
            engineText = EngineText.text;
#endif

            // Keep it for later, the rest of the scenes are going to need this cutie
            EngineXML = engineText;

            // Done, we simply load the next scene
            Application.LoadLevel(1);
        }
    }
}
