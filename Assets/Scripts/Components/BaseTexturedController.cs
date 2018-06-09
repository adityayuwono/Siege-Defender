using UnityEngine;
using UnityEngine.UI;

namespace Scripts.Components
{
	public class BaseTexturedController : BaseController
	{
		public Image MainTexture;

		protected Rect TextureScreenArea;

		protected override void OnSetup()
		{
			base.OnSetup();

			OnChange();
		}

		protected virtual void OnChange()
		{

		}
	}
}