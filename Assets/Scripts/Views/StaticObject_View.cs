using Scripts.ViewModels;
using UnityEngine;

namespace Scripts.Views
{
    public class StaticObject_View : RigidbodyView
    {
        private StaticObject_ViewModel _viewModel;
        public StaticObject_View(StaticObject_ViewModel viewModel, ObjectView parent) : base(viewModel, parent)
        {
            _viewModel = viewModel;
        }

        protected override void OnLoad()
        {
            base.OnLoad();

            Freeze();

            Transform.localScale = _assetScale;
            Transform.localRotation = Quaternion.identity;
        }
    }
}
