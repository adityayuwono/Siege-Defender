using Scripts.Components;
using Scripts.ViewModels.GUIs;
using Scripts.Views.Enemies;
using UnityEngine.UI;

namespace Scripts.Views.GUIs
{
	public class HealthBarView : ObjectView
	{
		private HealthBar _viewModel;
		private AlwaysFaceMainCamera _positionComponent;
		private Image _image;

		public HealthBarView(HealthBar viewModel, ObjectView parent)
			: base(viewModel, parent)
		{
			_viewModel = viewModel;
		}

		protected override void OnLoad()
		{
			base.OnLoad();

			_positionComponent = GameObject.AddComponent<AlwaysFaceMainCamera>();
			_image = GameObject.GetComponent<Image>();
		}

		protected override void OnShow()
		{
			base.OnShow();

			var livingObject = _viewModel.Root.GetView<LivingObjectView>(_viewModel.LivingObject);
			_positionComponent.Target = livingObject.HealthBarRoot;

			_viewModel.LivingObject.Health.OnChange += UpdateHealthBar;
			UpdateHealthBar();
		}

		protected override void OnHide(string reason)
		{
			_viewModel.LivingObject.Health.OnChange -= UpdateHealthBar;

			base.OnHide(reason);
		}

		private void UpdateHealthBar()
		{
			_image.fillAmount = _viewModel.LivingObject.HealthPercentage;
		}
	}
}
