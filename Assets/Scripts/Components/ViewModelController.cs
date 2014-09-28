using UnityEngine;
using Object = Scripts.ViewModels.Object;

namespace Scripts.Components
{
    public class ViewModelController : MonoBehaviour
    {
        public Object ViewModel;

        public Object GetViewModel()
        {
            return ViewModel;
        }
    }
}
