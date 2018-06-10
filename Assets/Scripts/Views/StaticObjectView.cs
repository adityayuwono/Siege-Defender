using Scripts.ViewModels;
using UnityEngine;

namespace Scripts.Views
{
	public class StaticObjectView : RigidbodyView
	{
		public StaticObjectView(StaticObject viewModel, ObjectView parent) : base(viewModel, parent)
		{
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