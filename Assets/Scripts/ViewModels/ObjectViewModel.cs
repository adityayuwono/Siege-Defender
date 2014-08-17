﻿using System;
using System.Collections.Generic;
using Scripts.Helpers;
using Scripts.Models;
using UnityEngine;

namespace Scripts.ViewModels
{
    public class ObjectViewModel : BaseViewModel
    {
        private readonly ObjectModel _model;

        public ObjectViewModel(ObjectModel model, BaseViewModel parent) : base(model, parent)
        {
            _model = model;

            if (string.IsNullOrEmpty(_model.AssetId))
                throw new EngineException(this, "No Asset defined");
        }

        protected readonly List<ObjectViewModel> Children = new List<ObjectViewModel>();

        protected override void OnActivate()
        {
            base.OnActivate();

            foreach (var child in Children)
                child.Activate();
        }

        public override void Show()
        {
            base.Show();

            foreach (var child in Children)
                child.Show();
        }

        public override void Hide()
        {
            base.Hide();

            //foreach (var child in Children)
            //    child.Hide();
        }

        protected override void OnDeactivate()
        {
            //foreach (var child in Children)
            //    child.Deactivate();

            base.OnDeactivate();
        }

        public virtual float DeathDelay
        {
            get { return 0f; }
        }
        public Action<ObjectViewModel> DoDestroy;
        protected void Destroy()
        {
            if (DoDestroy != null)
                DoDestroy(this);

            DoDestroy = null;
        }


        public string AssetId
        {
            get { return _model.AssetId; }
        }

        public Vector3 Position
        {
            get { return StringToVector3(_model.Position); }
        }

        protected Vector3 StringToVector3(string string3)
        {
            var splitted = string3.Split(',');
            var splitFloat = new float[splitted.Length];

            for (var i = 0; i < splitted.Length; i++)
            {
                splitFloat[i] = float.Parse(splitted[i]);
            }

            return new Vector3(splitFloat[0], splitFloat[1], splitFloat[2]);
        }
    }
}
