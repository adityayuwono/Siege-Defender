using System;
using Scripts.Core;
using Scripts.Models;

namespace Scripts.ViewModels
{
	public class Item : Object
	{
		public Action<Object> ParentChanged;

		public AdjustableProperty<string> BaseName;
		public AdjustableProperty<string> Stats;

		private readonly ItemModel _model;

		public Item(ItemModel model, Base parent)
			: base(model, parent)
		{
			_model = model;

			Stats = new AdjustableProperty<string>("Stats", this);
			BaseName = new AdjustableProperty<string>("BaseName", this);

			BaseName.SetValue(_model.BaseItem);
		}

		public string BaseItem
		{
			get { return _model.BaseItem; }
		}

		public ItemModel Model
		{
			get { return _model; }
		}

		public void ChangeParent(Object newParent)
		{
			Parent = newParent;

			if (ParentChanged != null)
			{
				ParentChanged(newParent);
			}
		}

		public void Select()
		{
			GetParent<Inventory>().SelectItem(this);
		}
	}
}