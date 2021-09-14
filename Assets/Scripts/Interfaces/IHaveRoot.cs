using Scripts.Roots;

namespace Scripts.Interfaces
{
	public interface IHaveRoot : IBaseView
	{
		IRoot Root { get; }
		GameRoot SDRoot { get; }

		T GetParent<T>() where T : class, IBaseView;
	}
}
