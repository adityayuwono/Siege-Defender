﻿using System;
using Scripts.Models.Items;

namespace Scripts.ViewModels.Items
{
	public class Item : Object
	{
		public Action<Object> ParentChanged;

		private readonly ItemModel _model;

		public Item(ItemModel model, Base parent)
			: base(model, parent)
		{
			_model = model;

			BaseName = _model.BaseItem;
		}

		public string BaseItem
		{
			get { return _model.BaseItem; }
		}

		public ItemModel Model
		{
			get { return _model; }
		}

		public string BaseName { get; protected set; }
		public string Stats { get; protected set; }
		public string Numbers { get; protected set; }
		public string Augmentation { get; set; }

		public string ItemSlotRoots
		{
			get { return _model.ItemSlotRoots; }
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