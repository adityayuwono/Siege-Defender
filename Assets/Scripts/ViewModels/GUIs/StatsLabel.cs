using System;
using Scripts.Core;
using Scripts.Models.GUIs;
using Scripts.ViewModels.Items;

namespace Scripts.ViewModels.GUIs
{
	public class StatsLabel : Static
	{
		public Action ItemUpdate;

		private readonly StatsLabelModel _model;
		private AdjustableProperty<ProjectileItem> _boundProjectileItem;

		public StatsLabel(StatsLabelModel model, Base parent) : base(model, parent)
		{
			_model = model;
		}

		public string Name { get; private set; }
		public string StatNames { get; private set; }
		public string StatNumbers { get; private set; }
		public string StatAugmentation { get; private set; }

		public override void Show()
		{
			base.Show();

			HandlePropertyChanged();
		}

		protected override void OnLoad()
		{
			base.OnLoad();

			if (_model.Source.StartsWith("{"))
			{
				_boundProjectileItem = Root.PropertyLookup.GetProperty(_model.Source) as AdjustableProperty<ProjectileItem>;
				_boundProjectileItem.OnChange += HandlePropertyChanged;
			}
		}

		protected override void OnDestroyed()
		{
			if (_boundProjectileItem != null)
			{
				_boundProjectileItem.OnChange -= HandlePropertyChanged;
			}

			base.OnDestroyed();
		}

		private void HandlePropertyChanged()
		{
			var projectileItem = _boundProjectileItem.GetValue();

			Name = projectileItem.BaseName;
			StatNames = projectileItem.Stats;
			StatNumbers = projectileItem.Numbers;
			StatAugmentation = projectileItem.Augmentation;

			if (ItemUpdate != null)
			{
				ItemUpdate();
			}
		}
	}
}
