using Scripts.Models.GUIs;

namespace Scripts.ViewModels.GUIs
{
	public class GameEndStats : LabelGUI
	{
		public GameEndStats(GameEndStatsModel model, Base parent)
			: base(model, parent)
		{
		}

		protected override void OnLoad()
		{
			base.OnLoad();
		}

		protected override void OnActivate()
		{
			base.OnActivate();

			Text.SetValue(string.Format("Accuracy: {0}%\nEnemies Killed: {1}", 
				Scripts.GameEndStats.GetAccuracy(),
				Scripts.GameEndStats.GetEnemiesKilled()));
		}
	}
}
