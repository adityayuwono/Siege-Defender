using System;
using Scripts.Enums;
using UnityEngine;

namespace Scripts.AnimatorBehaviours
{
	public class AnimationEvent : StateMachineBehaviour
	{
		public Action<AnimationEventType> Invoke;
		public AnimationEventType EventType;

		// OnStateExit is called when a transition ends and the state machine finishes evaluating this state
		public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
		{
			if (Invoke != null)
			{
				Invoke(EventType);
			}
		}
	}
}
