using Scripts.Components;
using UnityEngine;

namespace Scripts
{
	public class BaseContext : BaseController
	{
		[HideInInspector]
		public IntervalRunner IntervalRunner;

		#region Error Debug
		// Used to display error to the user, not going to be included in the actual release
		private string _lastErrorMessage;
		/// <summary>
		/// Show error in-game Screen, for debugging purposes only!
		/// </summary>
		/// <param name="message"></param>
		public void ThrowError(string message)
		{
			_lastErrorMessage = message;
		}

		private void OnGUI()
		{
			// TODO: Remove this in actual build, OnGUI is heavy in mobile
			if (!string.IsNullOrEmpty(_lastErrorMessage))
				GUI.Label(new Rect(0, 50, Screen.width, Screen.height), _lastErrorMessage);
		}
		#endregion
	}
}
