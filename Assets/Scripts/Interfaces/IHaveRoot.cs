using Scripts.Roots;

namespace Scripts.Interfaces
{
	public interface IHaveRoot : IBase
	{
		IRoot Root { get; }
		GameRoot SDRoot { get; }

		T GetParent<T>() where T : class, IBase;
	}
}
