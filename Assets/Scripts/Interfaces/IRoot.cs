using System.Collections;
using Scripts.Contexts;

namespace Scripts.Interfaces
{
	public interface IRoot : IContext, IViewModelLookup, IViewLookup
	{
		BaseContext Context { get; }
		IIntervalRunner IntervalRunner { get; }
		IResource ResourceManager { get; }

		void StartCoroutine(IEnumerator coroutine);
		void ChangeScene(string sceneName, string levelId);
	}
}