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
			Size = 40;
			Text.SetValue(string.Format("Accuracy: {0}%\nEnemies Killed: {1}\n\nTotal Damage: {2}\nDPS: {3}",
				Scripts.GameEndStats.GetAccuracy(),
				Scripts.GameEndStats.GetEnemiesKilled(),
				Scripts.GameEndStats.GetTotalDamage(),
				Scripts.GameEndStats.GetDPS()));
		}
	}
}
