using UnityEngine;
using System.Collections;

public class Boom : MonoBehaviour {

	public AudioClip[] boomA;

	void OnTriggerStay (Collider other){
		if (other.transform.tag == "Zombie") {
			other.GetComponent<ZombieMove>().helth -= 250;
			GameObject.Find ("Actor").GetComponent<Actor> ().count += 110;
		}
	}

	void Update() {
		//GetComponent<AudioSource> ().PlayOneShot (boomA [Random.Range (0, boomA.Length)]);
		Destroy (gameObject, 0.1f);
	}
}
