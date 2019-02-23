using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MuertoSoldier : StateMachineBehaviour {

	private Player player;
	private float elapsedTime;

	// OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
	override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
		player = animator.gameObject.GetComponent<Player>();
		elapsedTime = 0.0f;
	}

	// OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
	override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
		//TODO: Medir con timeDeltaTime hasta que pasen X segundos, donde el soldado muere definitivamente, dejando su tumba
		elapsedTime += Time.deltaTime;

		if (elapsedTime >= 30.0f){
			Vector3 posPlayer = player.transform.position;
			//Instantiate(); tumba
			//Pos tumba = posPlayer;
			Destroy(player);
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
