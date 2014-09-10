﻿using Scripts.Models;

namespace Scripts.ViewModels.Actions
{
    public class Base_ActionViewModel : BaseViewModel
    {
        public Base_ActionViewModel(Base_Model model, BaseViewModel parent) : base(model, parent)
        {
        }

        public virtual void Invoke()
        {
            throw new System.NotImplementedException();
        }
    }
}
