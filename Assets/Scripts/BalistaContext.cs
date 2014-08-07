﻿using CEP.Core;
using Scripts.Components;
using Scripts.Models;
using UnityEngine;

namespace Scripts
{
    public class BalistaContext : BaseController
    {
        public TextAsset EngineText;

        private void Start()
        {
            Physics.IgnoreLayerCollision(9,9);

            var engineModel = Deserializer<EngineModel>.GetObjectFromXML(EngineText.text);

            var engine = new BalistaShooter(engineModel, null);
            engine.MapInjections();
            engine.Activate();
            engine.Show();
        }
    }
}
