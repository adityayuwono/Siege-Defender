namespace Scripts.Interfaces
{
	public interface IAnalytics
	{
		void LogEvent(string eventCategory, string eventAction, string eventLabel, long value);
	}
}