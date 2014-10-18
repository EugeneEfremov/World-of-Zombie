using UnityEngine;
using System.Collections;

public class Skateboard : MonoBehaviour {

	void Start () {
	
	}
	
	void Update () {
        if (transform.rigidbody)
        {
            transform.rigidbody.AddForce(transform.forward * 50 * Input.GetAxis("Vertical"));
            transform.rigidbody.AddForce(transform.right * 100 * Input.GetAxis("Horizontal"));
        }
	}
}
