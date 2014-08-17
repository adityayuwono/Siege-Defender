using Scripts.ViewModels;
using UnityEngine;

namespace Scripts.Components
{
    public class ViewModelController : MonoBehaviour
    {
        public ObjectViewModel ViewModel;

        public ObjectViewModel GetViewModel()
        {
            return ViewModel;
        }
    }
}
