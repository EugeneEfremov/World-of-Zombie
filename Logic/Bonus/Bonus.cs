using UnityEngine;
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
                case "firegunBul50":
                    Actor.GetComponent<Weapons>().firegunBullet += 50;
                break;
                case "zeusgunBul50":
                    Actor.GetComponent<Weapons>().zeusgunBullet += 50;
                break;
                case "plasmicgunBul50":
                    Actor.GetComponent<Weapons>().plasmicgunBullet += 50;
                break;
                case "gaussgunBul100":
                    Actor.GetComponent<Weapons>().gaussgunBullet += 100;
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
                case "firegun":
                    Actor.GetComponent<Weapons>().firegunC = true;
                    Actor.GetComponent<Weapons>().GBfiregun = true;
                break;
                case "zeusgun":
                    Actor.GetComponent<Weapons>().zeusgunC = true;
                    Actor.GetComponent<Weapons>().GBzeusgun = true;
                break;
                case "plasmicgun":
                    Actor.GetComponent<Weapons>().plasmicgunC = true;
                    Actor.GetComponent<Weapons>().GBplasmicgun = true;
                break;
                case "gaussgun":
                    Actor.GetComponent<Weapons>().gaussgunC = true;
                    Actor.GetComponent<Weapons>().GBgaussgun = true;
                break;

                //Способности
                case "accuracyMax":
                    Actor.GetComponent<Weapons>().accuracyMax += 1;
                break;
                case "strongMax":
                    Actor.GetComponent<Weapons>().AlterMaxBullet(Actor.GetComponent<Weapons>().strongMax + 1);
                break;
                case "speedMax":
                    Actor.GetComponent<Actor>().speed += 2;
                break;
                case "helthMax":
                    Actor.GetComponent<Actor>().helthMax += 40;
                break;

                case "money10":
                    Actor.GetComponent<Global>().money += 10;
                break;
			}
			Destroy(gameObject);
		}
	}
}
