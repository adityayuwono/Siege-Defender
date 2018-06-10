using System.Collections.Generic;
using Scripts.Core;
using Scripts.Models;

namespace Scripts.ViewModels
{
	public abstract class IntervalBase : RandomPositionManager
	{
		private static bool _isDestructionInProgress;

		protected readonly Dictionary<string, List<Object>> InactiveObjects = new Dictionary<string, List<Object>>();
		public readonly Property<float> Interval = new Property<float>();

		protected IntervalBase(IntervalModel model, Base parent) : base(model, parent)
		{
		}

		protected void DestroyInactiveObjects()
		{
			if (_isDestructionInProgress)
			{
				return;
			}

			_isDestructionInProgress = true;
			foreach (var inactiveObjects in InactiveObjects.Values)
			{
				foreach (var inactiveObject in inactiveObjects)
				{
					inactiveObject.Destroy();
				}
			}

			InactiveObjects.Clear();
			_isDestructionInProgress = false;
		}
	}
}