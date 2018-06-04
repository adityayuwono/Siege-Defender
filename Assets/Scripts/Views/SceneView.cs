﻿using Scripts.ViewModels;
using UnityEngine;

namespace Scripts.Views
{
    public class SceneView : ObjectView
    {
	    private Scene _viewModel;

        public SceneView(Scene viewModel, ObjectView parent) : base(viewModel, parent)
        {
	        _viewModel = viewModel;
        }

	    protected override GameObject GetGameObject()
	    {
			var gameObject = GameObject.Find(_viewModel.AssetId);
		    return gameObject;
	    }

	    protected override Transform GetParent()
	    {
		    return null;
	    }
    }
}
