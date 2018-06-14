using Scripts.ViewModels.GUIs;
using UnityEngine.UI;

namespace Scripts.Views.GUIs
{
	public class StatsLabelView : StaticView
	{
		private readonly StatsLabel _viewModel;

		private Text _baseNameText;
		private Text _statNamesText;
		private Text _statNumbersText;
		private Text _augmentationText;

		public StatsLabelView(StatsLabel viewModel, ObjectView parent) : base(viewModel, parent)
		{
			_viewModel = viewModel;
			viewModel.ItemUpdate += ShowItemStats;
		}

		protected override void OnLoad()
		{
			base.OnLoad();

			_baseNameText = Transform.Find("BaseName").GetComponent<Text>();
			_statNamesText = Transform.Find("StatNames").GetComponent<Text>();
			_statNumbersText = Transform.Find("StatNumbers").GetComponent<Text>();
			_augmentationText = Transform.Find("StatAugmentation").GetComponent<Text>();

			ShowItemStats();
		}

		private void ShowItemStats()
		{
			_baseNameText.text = _viewModel.Name;
			_statNamesText.text = _viewModel.StatNames;
			_statNumbersText.text = _viewModel.StatNumbers;
			_augmentationText.text = _viewModel.StatAugmentation;
		}
	}
}
