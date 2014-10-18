using UnityEngine;
using System.Collections;

public class Deltaplan : MonoBehaviour {

    public Transform Right, Left;

	void Start () {
	
	}
	
	void Update () {
        if (transform.rigidbody)
        {
            if (transform.position.y > 10)
            {
                //Вперед
                transform.rigidbody.AddForce(-Right.up * 300);
                transform.rigidbody.AddForce(-Left.up * 300);
                //Повороты
                transform.rigidbody.AddForce(transform.right * 300 * Input.GetAxis("Horizontal"));
                transform.rigidbody.AddTorque(transform.up * 100 * Input.GetAxis("Horizontal"));
            }
            //Падение
            transform.rigidbody.AddForce( -transform.forward * 200);
        }
	}
}
