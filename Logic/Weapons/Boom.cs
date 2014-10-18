using UnityEngine;
using System.Collections;

public class Boom : MonoBehaviour {

	public AudioClip boomA;

	void OnTriggerStay (Collider other){
		if (other.transform.tag == "Zombie")
        {
			other.GetComponent<ZombieMove>().helth -= 250;
			GameObject.Find ("Actor").GetComponent<Actor> ().count += 110;
		}
        if (other.transform.tag == "Player")
        {
            other.GetComponent<Actor>().helth -= 20;
        }
	}

	void Start() {
		GetComponent<AudioSource> ().PlayOneShot (boomA);
		Destroy (gameObject, 0.7f);
	}
}
