using UnityEngine;
using System.Collections;

public class Barel : MonoBehaviour {

    public int helth = 100;
    public GameObject BoomObj, BoomTexture;
	
	void FixedUpdate () {
        if (helth < 1)
        {
            Instantiate(BoomObj, transform.position, Quaternion.Euler(0, 0, 0));
            Instantiate(BoomTexture, new Vector3(transform.position.x, transform.position.y - transform.localScale.y / 1.9f, transform.position.z), Quaternion.Euler(0, transform.rotation.y, transform.rotation.z));
            Destroy(gameObject);
        }
	}
}