﻿using UnityEngine;
using System.Collections;

public class Bonus : MonoBehaviour {
	public string typeBonus = ""; //helth20, gunBul50 и т.д.
	public GameObject Actor;

	void Start(){
		Actor = GameObject.Find ("Actor");
        //if (gameObject.transform.tag != "Weapons")
        Destroy(gameObject, 15);
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
                case "grenadeBul50":
                     Actor.GetComponent<Weapons>().grenadeBullet += 50;
                break;
                case "minigunBul500":
                     Actor.GetComponent<Weapons>().minigunBullet += 500;
                break;
                case "rocketBul50":
                    Actor.GetComponent<Weapons>().rocketBullet += 50;
                break;
                case "diskgunBul50":
                    Actor.GetComponent<Weapons>().diskgunBullet += 50;
                break;
                case "armour100":
                    Actor.GetComponent<Actor>().armour += 100;
                 break;


				//Оружие
				case "gun":
					Actor.GetComponent<Weapons>().gunC = true;
                    Actor.GetComponent<Weapons>().GBgun = true;
				break;
				case "grenade":
					Actor.GetComponent<Weapons>().grenadeC = true;
                    Actor.GetComponent<Weapons>().GBgrenade = true;
				break;
				case "minigun":
					Actor.GetComponent<Weapons>().minigunC = true;
                    Actor.GetComponent<Weapons>().GBminigun = true;
				break;
				case "rocket":
					Actor.GetComponent<Weapons>().rocketC = true;
                    Actor.GetComponent<Weapons>().GBrocket = true;
				break;
				case "diskgun":
					Actor.GetComponent<Weapons>().diskgunC = true;
                    Actor.GetComponent<Weapons>().GBdiskgun = true;
				break;
			}
			Destroy(gameObject);
		}
	}
}
