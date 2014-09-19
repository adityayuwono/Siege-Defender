using Scripts.Core;
using Scripts.Helpers;
using Scripts.Models;

namespace Scripts.ViewModels
{
    public class ObjectDisplay_ViewModel : IntervalViewModel<Object>
    {
        private readonly ObjectDisplay_Model _model;
        public ObjectDisplay_ViewModel(ObjectDisplay_Model model, Object parent) : base(model, parent)
        {
            _model = model;
        }

        public Property<string> ObjectIdBinding;
        public readonly Property<Object> CurrentObject = new Property<Object>(); 
        protected override void OnLoad()
        {
            base.OnLoad();

            ObjectIdBinding = Root.Binding.GetProperty<string>(_model.ObjectId);
            ObjectIdBinding.OnChange += ObjectId_OnChange;
        }

        public override void Show()
        {
            base.Show();

            ObjectId_OnChange();
        }


        private void ObjectId_OnChange()
        {
            var currentObjectVM = CurrentObject.GetValue();
            if (currentObjectVM != null)
                currentObjectVM.Hide("Changing displayed object");

            var newProjectile = GetObject<Object>(ObjectIdBinding.GetValue());
            newProjectile.Activate();
            newProjectile.Show();

            CurrentObject.SetValue(newProjectile);
        }

        protected override void OnDestroyed()
        {
            ObjectIdBinding.OnChange -= ObjectId_OnChange;
            ObjectIdBinding = null;

            CurrentObject.SetValue(null);

            base.OnDestroyed();
        }


        protected override Object SpawnNewObject(string id)
        {
            var modelToCopy = Root.GetObjectModel(id);
            var objectModel = Copier.CopyAs<Object_Model>(modelToCopy);
            
            objectModel.Id = string.Format("{0}_{1}_{2}", objectModel.Id, Id, ObjectCount);
            objectModel.Type = id;
            
            var newObject = new StaticObject_ViewModel(objectModel, this);
            
            return newObject;
        }
    }
}
