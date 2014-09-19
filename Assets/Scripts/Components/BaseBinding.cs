using Scripts.Core;

namespace Scripts.Components
{
    public class BaseBinding : BaseController
    {
        private Property _property;
        public void SetProperty(Property property)
        {
            _property = property;
        }

        private void Start()
        {
            Bind();
        }

        private void OnDestroy()
        {
            Unbind();
        }

        protected virtual void Bind()
        {
            _property.OnChange += OnChanged;
        }

        protected virtual void Unbind()
        {
            _property.OnChange -= OnChanged;
        }
        protected virtual void OnChanged() { }
    }
}
