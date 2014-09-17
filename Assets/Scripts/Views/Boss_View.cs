using Scripts.ViewModels.Enemies;

namespace Scripts.Views
{
    public class Boss_View : EnemyBaseView
    {
        private readonly Boss_ViewModel _viewModel;
        private readonly EnemyManagerView _parent;
        public Boss_View(Boss_ViewModel viewModel, EnemyManagerView parent) : base(viewModel, parent)
        {
            _viewModel = viewModel;
            _parent = parent;
        }

        protected override void SetPosition()
        {
            var randomPosition = _parent.GetRandomSpawnPoint();
            randomPosition.x = 0;
            randomPosition.z = 0;
            Transform.localPosition = randomPosition;
        }
    }
}
