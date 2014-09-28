namespace Scripts.Core
{
    public class BindingManager
    {
        private readonly EngineBase _mainEngine;
        public BindingManager(EngineBase engine)
        {
            _mainEngine = engine;
        }

        /// <summary>
        /// Go through the list on the Main Engine, then get the property needed
        /// </summary>
        /// <param name="path">string </param>
        /// <returns>May return null</returns>
        private Property GetProperty(string path)
        {
            if (path.StartsWith("{") && path.EndsWith("}"))
            {
                path = path.Replace("{", "").Replace("}", "");
                var bindingPath = path.Split('.');

                var foundProperty = _mainEngine.GetProperty(bindingPath[0], bindingPath[1]);

                return foundProperty;
            }

            return null;
        }

        public Property<T> GetProperty<T>(string path)
        {
            return GetProperty(path) as Property<T>;
        }
    }
}
