using Scripts.ViewModels.GUIs;
using UnityEngine;

namespace Scripts.Views.GUIs
{
	public class DamageView : LabelView
	{
		private readonly Damage _viewModel;
		private Vector3 _initialScale;

		public DamageView(Damage viewModel, ObjectView parent)
			: base(viewModel, parent)
		{
			_viewModel = viewModel;
		}

		protected override void OnLoad()
		{
			base.OnLoad();

			_initialScale = Transform.lossyScale;
		}

		protected override void OnHide(string reason)
		{
			_viewModel.Root.Context.IntervalRunner.SubscribeToInterval(Hide, _viewModel.HideDelay*2f, false);

			iTween.Stop(GameObject);
			Transform.localScale = _initialScale;
			iTween.MoveBy(GameObject, Vector3.up * 2f, _viewModel.HideDelay);
			iTween.ScaleBy(GameObject, Vector3.one * 1.25f, _viewModel.HideDelay);
		}

		private void Hide()
		{
			_viewModel.Root.Context.IntervalRunner.UnsubscribeFromInterval(Hide);

			base.OnHide("Hiding DamageGUI");
		}
	}
}