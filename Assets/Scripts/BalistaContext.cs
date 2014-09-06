using Scripts.Components;
using Scripts.Helpers;
using Scripts.Models;
using UnityEngine;

namespace Scripts
{
    /// <summary>
    /// Main Component as a bridge between the Engine to Unity
    /// </summary>
    public class BalistaContext : BaseController
    {
        /// <summary>
        /// Singleton, YEAH!!!
        /// </summary>
        public static BalistaContext Instance { get { return _instance; } }
        private static BalistaContext _instance;

        private void Awake()
        {
            // Initialize singleton
            _instance = this;
        }

        private void Start()
        {
            Physics.IgnoreLayerCollision(9,9);// Layer 9 will not collide with layer 9

            // Get XML from Splash, simple stuff
            var engineText = XMLInitializer.EngineXML;
            var engineModel = Deserializer<EngineModel>.GetObjectFromXML(engineText);
            
            // Start ann instance of the Engine
            var engine = new BalistaShooter(engineModel, Instance);
            engine.MapInjections();
            engine.Activate();
            engine.Show();
        }

        #region Error Debug
        // Used to display error to the user, not going to be included in the actual release
        private string _lastErrorMessage;
        /// <summary>
        /// Show error in-game Screen, for debugging purposes only!
        /// </summary>
        /// <param name="message"></param>
        public void ThrowError(string message)
        {
            _lastErrorMessage = message;
        }
        private void OnGUI()
        {
            if (!string.IsNullOrEmpty(_lastErrorMessage))
            {
                GUI.Label(new Rect(0,50,Screen.width, Screen.height), _lastErrorMessage);
            }
        }
        #endregion

        public void OnClicked(string s)
        {
            Debug.LogError(s);
        }
    }
}
