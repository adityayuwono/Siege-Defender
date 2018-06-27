using Scripts.Helpers;
using Scripts.Interfaces;
using Scripts.Models.Actions;

namespace Scripts.ViewModels.Actions
{
	public class TargetProperty : Base
	{
		private readonly TargetPropertyModel _model;

		protected TargetProperty(TargetPropertyModel model, Base parent) : base(model, parent)
		{
			_model = model;

			if (string.IsNullOrEmpty(_model.Target))
			{
				throw new EngineException(this, 
					string.Format("{0} does not have a Target defined", FullId));
			}
		}

		protected object Target { get; private set; }

		protected override void OnLoad()
		{
			base.OnLoad();

			Target = FindTarget(_model.Target);
		}

		protected virtual object FindTarget(string targetPath)
		{
			var parentContext = GetParent<IContext>();
			if (parentContext == null)
			{
				throw new EngineException(this, "Failed to find parent Context");
			}

			var binding = parentContext.PropertyLookup.GetBinding(targetPath);
			if (binding != null)
			{
				return binding.Get();
			}
#if UNITY_EDITOR
			else
			{
				// Do it again to debug in editor
				binding = parentContext.PropertyLookup.GetBinding(targetPath);
			}
#endif


			throw new EngineException(this,
				string.Format("Failed to find Target: {0}", targetPath));
		}
	}
}