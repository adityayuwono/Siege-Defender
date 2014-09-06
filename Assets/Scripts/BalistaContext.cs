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
        private void Start()
        {
            Physics.IgnoreLayerCollision(9,9);// Layer 9 will not collide with layer 9

            var engineText = XMLInitializer.EngineXML;
            var engineModel = Deserializer<EngineModel>.GetObjectFromXML(engineText);

            var engine = new BalistaShooter(engineModel, this);
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
    }
}
