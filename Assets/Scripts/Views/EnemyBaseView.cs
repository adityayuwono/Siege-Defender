using Scripts.Components;
using Scripts.ViewModels;

namespace Scripts.Views
{
    public class EnemyBaseView : ObjectView
    {
        private readonly EnemyBaseViewModel _viewModel;
        private readonly EnemyManagerView _parent;

        public EnemyBaseView(EnemyBaseViewModel viewModel, EnemyManagerView parent) : base(viewModel, parent)
        {
            _viewModel = viewModel;
            _parent = parent;
        }

        protected override void OnShow()
        {
            base.OnShow();

            var baseController = AttachController<EnemyBaseController>();
            GameObject.transform.position = _parent.GetRandomSpawnPoint();
        }
    }
}
