using UnityEngine;
using System.Collections;

public class Barel : MonoBehaviour {

    public int helth = 100;
    public GameObject BoomObj;
	
	void FixedUpdate () {
        if (helth < 1)
        {
            Instantiate(BoomObj, transform.position, Quaternion.Euler(0, 0, 0));
            Destroy(gameObject);
        }
	}
}