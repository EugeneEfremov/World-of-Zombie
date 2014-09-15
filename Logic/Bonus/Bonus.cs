using UnityEngine;
using System.Collections;

public class Bonus : MonoBehaviour {
	public string typeBonus = ""; //helth20, 
	public GameObject Actor;

	void Start(){
		Actor = GameObject.Find ("Actor");
		Destroy (gameObject, 15);
	}

	void OnTriggerEnter(Collider other){
		if (other.transform.tag == "Player") {
			switch (typeBonus){
				case "helth20":
					Actor.GetComponent<Actor>().helth += 20;
				break;
				case "gunBul50":
					Actor.GetComponent<Weapons>().gunBullet += 50;
				break;

				//Оружие
				case "gun":
					Actor.GetComponent<Weapons>().gunC = true;
				break;
				case "grenade":
					Actor.GetComponent<Weapons>().grenadeC = true;
				break;
				case "minigun":
					Actor.GetComponent<Weapons>().minigunC = true;
				break;
				case "rocket":
					Actor.GetComponent<Weapons>().rocketC = true;
				break;
				case "diskgun":
					Actor.GetComponent<Weapons>().diskgunC = true;
				break;
			}
			Destroy(gameObject);
		}
	}
}
