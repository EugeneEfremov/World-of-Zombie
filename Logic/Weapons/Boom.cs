using UnityEngine;
using System.Collections;

public class Boom : MonoBehaviour {

	void OnTriggerStay (Collider other){
		if (other.transform.tag == "Zombie") {
			other.GetComponent<ZombieMove>().helth -= 250;
			GameObject.Find ("Actor").GetComponent<Actor> ().count += 110;
		}
	}

	void Update() {
		Destroy (gameObject, 0.1f);
	}
}
