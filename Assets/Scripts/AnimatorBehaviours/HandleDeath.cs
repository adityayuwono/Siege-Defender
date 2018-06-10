using System;
using UnityEngine;

namespace Scripts.AnimatorBehaviours
{
	public class HandleDeath : StateMachineBehaviour
	{
		public Action HandleDeathEnd;

		// OnStateExit is called when a transition ends and the state machine finishes evaluating this state
		public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
		{
			if (HandleDeathEnd != null)
			{
				HandleDeathEnd();
			}
		}
	}
}
