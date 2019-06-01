using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AlertaSniper : StateMachineBehaviour {

	private Player player;
	private NavMeshAgent pAI;

	private float elapsedTime;
	public static float speed = 5.0f;

	// OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
	override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
		player = animator.gameObject.GetComponent<Player>();
		player.setLayerAnimator(layerIndex);
		pAI = player.getAgent();
	}

	// OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
	override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
		Aspect destination = player.getEnemySound();

        if (!destination)
            return;

		Vector3 focusLeveled = destination.transform.position;
		focusLeveled.y = player.transform.position.y;

		if (!lookingAt (player.transform.forward, player.transform.position, focusLeveled)) {

			Vector3 targetDir = focusLeveled - player.transform.position;

			// The step size is equal to speed times frame time.
			float step = speed * Time.deltaTime;

			Vector3 newDir = Vector3.RotateTowards(player.transform.forward, targetDir, step, 0.0f);

			// Move our position a step closer to the target.
			player.transform.rotation = Quaternion.LookRotation(newDir);
		}
	}

	private bool lookingAt(Vector3 direction, Vector3 position, Vector3 targetPos){

		Vector3 desiredDirection = targetPos - position;
		float angle = Vector3.Angle( desiredDirection, direction );
		return angle < 5.0f;

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
