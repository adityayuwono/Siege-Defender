using Scripts.Models.GUIs;

namespace Scripts.ViewModels.GUIs
{
	public class GameEndStats : LabelGUI
	{
		public GameEndStats(GameEndStatsModel model, Base parent)
			: base(model, parent)
		{
		}

		protected override void OnActivate()
		{
			base.OnActivate();
			Size = 40;
			Text.SetValue(string.Format("Accuracy: {0}%\nEnemies Killed: {1}\n\nTotal Damage: {2}\nDPS: {3}",
				GameEndStatsManager.GetAccuracy(),
				GameEndStatsManager.GetEnemiesKilled(),
				GameEndStatsManager.GetTotalDamage(),
				GameEndStatsManager.GetDPS()));
		}
	}
}
