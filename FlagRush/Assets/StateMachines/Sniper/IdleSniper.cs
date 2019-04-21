using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class IdleSniper : StateMachineBehaviour {

	private Sniper player;
	private NavMeshAgent pAI;

	private float elapsedTime;
	private PositionBarricade targeting;

	private float lookAt;

	// OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
	override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
		player = animator.gameObject.GetComponent<Sniper>();
		pAI = player.getAgent();
		pAI.ResetPath ();

		lookAt = 0;

		targeting = null;
		elapsedTime = 0;
	}

	// OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
	override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
		lookAt += Time.deltaTime;

		if (lookAt > 3) {
			lookAt = 0;

			if (pAI.velocity == Vector3.zero)
				player.transform.LookAt (player.barricade.getPositionMarker(player.positionBarricade));
		}

		if (player.positionBarricade % 4 == 0) {
			elapsedTime += Time.deltaTime;

			if (elapsedTime >= 15.0f) {
				if (targeting == null) {
					targeting = player.barricade.attack (player);

					if (targeting != null) {
						pAI.SetDestination (targeting.transform.position);
					}
				}
			}
		}
	}

	// OnStateExit is called when a transition ends and the state machine finishes evaluating this state
	//override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
	//
	//}

	// OnStateMove is called right after Animator.OnAnimatorMove(). Code that processes and affects root motion should be implemented here
	//override public void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
	//
	//}

	// OnStateIK is called right after Animator.OnAnimatorIK(). Code that sets up animation IK (inverse kinematics) should be implemented here.
	//override public void OnStateIK(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
	//
	//}
}
