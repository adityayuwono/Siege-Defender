using System;
using System.Collections;
using Scripts.Contexts;
using Scripts.Interfaces;
using Scripts.Models;
using Scripts.ViewModels;

namespace Scripts
{
	public class MenuRoot : RootBase
	{
		private readonly IIntervalRunner _intervalRunner;
		private readonly MenuRootModel _model;

		public MenuRoot(MenuRootModel model, BaseContext parent)
			: base(model, parent)
		{
			_model = model;
			_intervalRunner = new EmptyIntervalRunner();
		}

		public override IIntervalRunner IntervalRunner
		{
			get { return _intervalRunner; }
		}

		public override IRoot Root
		{
			get { return this; }
		}

		public override void StartCoroutine(IEnumerator coroutine)
		{
			throw new NotImplementedException();
		}

		protected override void OnActivate()
		{
			base.OnActivate();

			var scene = new Scene(_model.SceneModel, this);
			scene.Activate(DataContext.LevelId);
			scene.Show();
		}
	}
}