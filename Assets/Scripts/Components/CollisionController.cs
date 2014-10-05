﻿using System;
using UnityEngine;
using Object = Scripts.ViewModels.Object;

namespace Scripts.Components
{
    public class CollisionController : MonoBehaviour
    {
        public Action<Object, Vector3, Vector3> OnCollision;

        private void OnCollisionEnter(Collision collisionInfo)
        {
            if (OnCollision != null)
            {
                var viewModelController = collisionInfo.gameObject.GetComponent<ViewModelController>();
                if (viewModelController != null)
                {
                    var viewModel = viewModelController.GetViewModel();
                    OnCollision(viewModel, transform.position, collisionInfo.contacts[0].point);
                }
            }
        }

        private void OnTriggerEnter(Collider target)
        {
            if (OnCollision != null)
            {
                var viewModelController = target.gameObject.GetComponent<ViewModelController>();
                if (viewModelController != null)
                {
                    var viewModel = viewModelController.GetViewModel();
                    
                    // Crude calculation of collision point, need to improve later
                    // TODO: Improve this
                    var collisionPosition = transform.position;
                    var targetPosition = target.transform.position;
                    //collisionPosition.x = targetPosition.x;
                    //collisionPosition.z = targetPosition.z;

                    OnCollision(viewModel, transform.position, collisionPosition);
                }
            }
        }
    }
}
