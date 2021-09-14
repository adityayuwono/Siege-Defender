using System;
using Scripts.Contexts;
using Scripts.Interfaces;

namespace Scripts.Helpers
{
	public class EngineException : Exception
	{
		public EngineException(IBaseView baseObject, string message) :
			base(string.Format("{0}({1})\n{2}\nat: {3}", baseObject.GetType(), baseObject.FullId, message, 0))
		{
			var errorMessage = string.Format("{0}({1}): {2}\nat: {3}", baseObject.GetType(), baseObject.Id, message, 0);
			BaseContext.Instance.ThrowError(errorMessage);
		}
	}
}