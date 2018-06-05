﻿using System;
using Scripts.Models;

namespace Scripts.ViewModels
{
	public class Item : Object
	{
		private readonly ItemModel _model;
		public Action<Object> OnParentChanged;

		public Item(ItemModel model, Base parent)
			: base(model, parent)
		{
			_model = model;
		}

		public string Base
		{
			get { return _model.Base; }
		}

		public ItemModel Model
		{
			get { return _model; }
		}

		public void ChangeParent(Object newParent)
		{
			Parent = newParent;

			if (OnParentChanged != null) OnParentChanged(newParent);
		}
	}
}