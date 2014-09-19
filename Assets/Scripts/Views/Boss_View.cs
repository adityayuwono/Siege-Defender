using Scripts.ViewModels.Enemies;

namespace Scripts.Views
{
    public class Boss_View : EnemyBaseView
    {
        private readonly Boss _viewModel;
        private readonly EnemyManagerView _parent;
        public Boss_View(Boss viewModel, EnemyManagerView parent) : base(viewModel, parent)
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
