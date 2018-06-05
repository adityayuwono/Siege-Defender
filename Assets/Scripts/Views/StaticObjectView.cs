using Scripts.ViewModels;
using UnityEngine;

namespace Scripts.Views
{
	public class StaticObjectView : RigidbodyView
	{
		private StaticObject _viewModel;

		public StaticObjectView(StaticObject viewModel, ObjectView parent) : base(viewModel, parent)
		{
			_viewModel = viewModel;
		}

		protected override void OnLoad()
		{
			base.OnLoad();

			Freeze();

			Transform.localScale = AssetScale;
			Transform.localRotation = Quaternion.identity;
		}
	}
}