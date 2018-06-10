﻿using Scripts.ViewModels.GUIs;
using UnityEngine;

namespace Scripts.Views.GUIs
{
	public class DamageGUIView : LabelGUIView
	{
		private readonly DamageGUI _viewModel;
		private Vector3 _initialScale;

		public DamageGUIView(DamageGUI viewModel, ObjectView parent)
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
			iTween.MoveBy(GameObject, Vector3.up * 2f, _viewModel.HideDelay);
		}

		private void Hide()
		{
			_viewModel.Root.Context.IntervalRunner.UnsubscribeFromInterval(Hide);

			base.OnHide("Hiding DamageGUI");
		}
	}
}