using Scripts.Core;

namespace Scripts.Interfaces
{
	public interface IContext : IHaveRoot
	{
		PropertyLookup PropertyLookup { get; }
	}
}