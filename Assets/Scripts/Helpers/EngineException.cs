using System;
using Scripts.Interfaces;
using UnityEngine;

namespace Scripts.Helpers
{
    public class EngineException: Exception
    {
        public EngineException(IBase baseObject, string message) :
            base(string.Format("{0}({1}): {2}\nat: {3}", baseObject.GetType(), baseObject.Id, message, Time.realtimeSinceStartup))
        {
            BalistaShooter.Instance.ThrowError(string.Format("{0}({1}): {2}\nat: {3}", baseObject.GetType(), baseObject.Id, message, Time.realtimeSinceStartup));
        }
    }
}
