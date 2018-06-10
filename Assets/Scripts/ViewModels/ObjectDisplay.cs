using Scripts.Contexts;
using Scripts.Core;
using Scripts.Helpers;
using Scripts.Interfaces;
using Scripts.Models;

namespace Scripts.ViewModels
{
	public class ObjectDisplay : Interval<Object>
	{
		private readonly ObjectDisplayModel _model;
		public readonly Property<Object> CurrentObject = new Property<Object>();

		public Property<string> ObjectIdBinding;

		public ObjectDisplay(ObjectDisplayModel model, Object parent) : base(model, parent)
		{
			_model = model;
		}

		protected override void OnLoad()
		{
			base.OnLoad();

			ObjectIdBinding = GetParent<IContext>().PropertyLookup.GetProperty<string>(_model.ObjectId);
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
			{
				currentObjectVM.Hide("Changing displayed object");
			}

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


		protected override Object SpawnNewObject(string id, ObjectModel modelToCopy = null, Base overrideParent = null)
		{
			if (modelToCopy == null)
			{
				modelToCopy = DataContext.GetObjectModel(this, id);
			}
			var objectModel = Copier.CopyAs<ObjectModel>(modelToCopy);

			objectModel.Id = string.Format("{0}_{1}_{2}", objectModel.Id, Id, ObjectCount);
			objectModel.Type = id;

			var newObject = new StaticObject(objectModel, this);

			return newObject;
		}
	}
}