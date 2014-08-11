using System.Collections.Generic;
using Scripts.Models;

namespace Scripts.ViewModels
{
    public class SceneViewModel : ObjectViewModel
    {
        private readonly SceneModel _model;

        public SceneViewModel(SceneModel model, BaseViewModel parent) : base(model, parent)
        {
            _model = model;

            foreach (var elementModel in _model.Elements)
            {
                var elementVM = Root.IoCContainer.GetInstance<ElementViewModel>(elementModel.GetType(), new object[] { elementModel, this });

                _elements.Add(elementVM);
            }
        }

        protected override void OnActivate()
        {
            base.OnActivate();

            foreach (var elementViewModel in _elements)
                elementViewModel.Activate();
        }

        public override void Show()
        {
            base.Show();

            foreach (var elementViewModel in _elements)
                elementViewModel.Show();
        }

        private readonly List<ElementViewModel> _elements = new List<ElementViewModel>(); 
    }
}
