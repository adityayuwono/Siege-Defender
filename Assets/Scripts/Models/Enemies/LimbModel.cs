﻿using System;
using System.ComponentModel;
using System.Xml.Serialization;

namespace Scripts.Models.Enemies
{
    [Serializable]
    public class LimbModel : LivingObjectModel
    {
        [XmlAttribute]
        public string CollisionEffectOnBreak { get; set; }

        [XmlAttribute]
        public string CollisionEffectBroken { get; set; }

        [XmlAttribute]
        [DefaultValue(1f)]
        public float DamageMultiplier { get; set; }

        public LimbModel()
        {
            DamageMultiplier = 1f;
            ProjectileLimit = 2;
        }
    }
}
