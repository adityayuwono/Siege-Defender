using Scripts.Models;

namespace Scripts.ViewModels.Actions
{
    public class Action_ViewModel : BaseViewModel
    {
        public Action_ViewModel(BaseModel model, BaseViewModel parent) : base(model, parent)
        {
        }

        public virtual void Invoke()
        {
            throw new System.NotImplementedException();
        }
    }
}
