using UnityEngine;
using System.Collections;

public class Goal : MonoBehaviour {

    public Animation GoalAnim;
    public AnimationClip GoalAction;

	void Start () {
        GoalAnim.AddClip(GoalAction, "GoalAction");
        GoalAnim["GoalAction"].speed = 1;
	}
	
	void FixedUpdate () {
        GoalAnim.Play("GoalAction");
	}
}
