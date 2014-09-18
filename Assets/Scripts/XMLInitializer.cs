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
        public TextAsset EngineTextAsset;
        public TextAsset DefaultInventory;



        /// <summary>
        /// Text file that holds all the engine data
        /// </summary>
        public static string EngineXML;
        
        /// <summary>
        /// Text file that holds all the player's progress
        /// </summary>
        public static string InventoryXML;



        public void Start()
        {
            var loader = new WWW(FilePaths.Loading + "/Inventory.xml");
            EngineXML = EngineTextAsset.text;
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

            string inventoryText;
            if (loader.error != null || isUnityEditor)
            {
                // We don't have an XML yet, let's create one based on the template
                inventoryText = EngineTextAsset.text;
                using (var sw = new StreamWriter(loader.url))
                {
                    sw.Write(inventoryText);
                    sw.Close();
                }
            }
            else
                // Woohooo found the file
                inventoryText = loader.text;



            // Keep it for later, the rest of the scenes are going to need this cutie
            InventoryXML = inventoryText;

            // Done, we simply load the next scene
            Application.LoadLevel(1);
        }
    }
}
