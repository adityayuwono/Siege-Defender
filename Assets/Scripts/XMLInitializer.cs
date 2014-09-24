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
            EngineXML = EngineTextAsset.text;
            // Start preparing XML
            InitializeEngine();
        }

        private void InitializeEngine()
        {
            var inventoryText = "";
            // TODO: improve to cloud saving
            try
            {
                // Try to load save file, if any
                var sr = new StreamReader(FilePaths.Loading + "/Inventory.xml");
                inventoryText = sr.ReadToEnd();
                sr.Close();
            }
            catch (Exception ex)
            {
                // No save file yet, just create a default one
                inventoryText = DefaultInventory.text;
            }

            // Keep it for later, the rest of the scenes are going to need this cutie
            InventoryXML = inventoryText;

            // Done, we simply load the next scene
            Application.LoadLevel(1);
        }
    }
}
