using System.Collections;
using UnityEngine;

namespace Scripts.Components.SpecialEvents
{
    public class AirBalloonEventController : BaseSpecialEventController
    {
        public float Speed = 1;
        public Transform[] Waypoints;

        protected override IEnumerator EnumerateSpecialEvent()
        {
            foreach (var waypoint in Waypoints)
            {
                var distance = Vector3.Distance(transform.position, waypoint.position);
                var timeTakenToMoveToWaypoint = distance/Speed;
                iTween.MoveTo(gameObject, waypoint.position, timeTakenToMoveToWaypoint);
                yield return new WaitForSeconds(timeTakenToMoveToWaypoint);
            }

            if (OnSpecialEventFinished != null)
                OnSpecialEventFinished();
        }
    }
}
