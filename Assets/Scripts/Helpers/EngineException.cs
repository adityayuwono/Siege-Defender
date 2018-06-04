﻿using System;
using Scripts.Interfaces;

namespace Scripts.Helpers
{
    public class EngineException: Exception
    {
        public EngineException(IBase baseObject, string message) :
            base(string.Format("{0}({1})\n{2}\nat: {3}", baseObject.GetType(), baseObject.FullId, message, 0))
        {
            var errorMessage = string.Format("{0}({1}): {2}\nat: {3}", baseObject.GetType(), baseObject.Id, message, 0);
            //GameContext.Instance.ThrowError(errorMessage);
        }
    }
}
