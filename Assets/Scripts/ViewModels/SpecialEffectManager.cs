using System.Collections.Generic;
using Scripts.Helpers;
using Scripts.Models;
using UnityEngine;

namespace Scripts.ViewModels
{
	public class SpecialEffectManager : Interval<SpecialEffect>
	{
		private readonly SpecialEffectManagerModel _model;
		private readonly Dictionary<Object, SpecialEffect> _specialEffects = new Dictionary<Object, SpecialEffect>();

		public SpecialEffectManager(SpecialEffectManagerModel model, Object parent) : base(model, parent)
		{
			_model = model;
		}

		public void StartSpecialEffectOn(string id, Vector3 position)
		{
			var specialEffect = GetObject<SpecialEffect>(id);
			specialEffect.ShowSpecialEffect(position);
		}

		public void StartSpecialEffectOn(string id, Object viewModel)
		{
			var specialEffect = GetObject<SpecialEffect>(id);

			if (_specialEffects.ContainsKey(viewModel))
				_specialEffects[viewModel] = specialEffect;
			else
				_specialEffects.Add(viewModel, specialEffect);

			specialEffect.ShowSpecialEffect(viewModel);
		}

		public void StopSpecialEffectOn(Object viewModel)
		{
			if (!_specialEffects.ContainsKey(viewModel))
				throw new EngineException(this, string.Format("Failed to find SpecialEffect on {0}", viewModel.Id));
			var specialEffect = _specialEffects[viewModel];
			specialEffect.StopImmediatelly();
		}

		protected override void OnLoad()
		{
			base.OnLoad();

			((GameRoot) Root).SpecialEffectManager = this;
		}
	}
}