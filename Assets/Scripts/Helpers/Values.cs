namespace Scripts.Helpers
{
	public static class Values
	{
		public const float GuiCrosshairSizeF = 0.1f;

		public const int CrosshairLayermask = ~(1 << 9);

		public const char DamageDelimiter = '-';

		public static float GuiCrosshairHalfsizeF
		{
			get { return GuiCrosshairSizeF / 2f; }
		}

		public static class Defaults
		{
			public const string PlayerProgressFileName = "PlayerSettings.xml";

			public const string BossCharacterRootName = "Character";
			public const string WaypointTransformTag = "Waypoint";
		}
	}
}