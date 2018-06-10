namespace Scripts.Models.Actions
{
	public enum Event
	{
		None,
		Click,

		Interrupt,
		Break,

		Spawn,
		Walk,
		Attack,
		Death,
		DeathEnd,

		Hit,

		GameOver
	}
}