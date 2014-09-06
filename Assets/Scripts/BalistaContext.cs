using Scripts.Components;
using Scripts.Helpers;
using Scripts.Models;
using UnityEngine;

namespace Scripts
{
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

        private string _lastErrorMessage;
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
