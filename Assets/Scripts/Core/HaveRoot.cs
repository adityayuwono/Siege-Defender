using Scripts.Interfaces;
using Scripts.Roots;
using System;

namespace Scripts.Core
{
    public abstract class HaveRoot : IHaveRoot, IHaveId
    {
        private IHaveId _idContainer;

        protected HaveRoot(IHaveId idContainer, IHaveRoot parent)
        {
            _idContainer = idContainer;
            Parent = parent;

            // Generate a unique Id if there's none
            if (string.IsNullOrEmpty(_idContainer.Id))
            {
                _idContainer.Id = Guid.NewGuid().ToString();
            }
        }

        public virtual IRoot Root
        {
            get { return Parent.Root; }
        }

        public virtual GameRoot SDRoot
        {
            get { return Parent.SDRoot; }
        }

        public virtual string Id
        {
            get { return _idContainer.Id; }
            set { }
        }

        public virtual string FullId
        {
            get { return Parent != null ? Parent.FullId + "/" + Id : Id; }
        }

        public IHaveRoot Parent { get; protected set; }

        public T GetParent<T>() where T : class, IBaseView
        {
            if (Parent == null)
            {
                return null;
            }

            var parent = Parent as T;
            return parent ?? Parent.GetParent<T>();
        }
    }
}
