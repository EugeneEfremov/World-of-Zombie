using UnityEngine;
using System.Collections;

public class Bonus : MonoBehaviour {
    private Weapons weapons;

	public string typeBonus = ""; //helth20, gunBul50 и т.д.
	public GameObject Actor, ZombAll;

	void Start(){
		Actor = GameObject.Find ("Actor");
        ZombAll = GameObject.Find("ZombieLogic");
        Destroy(gameObject, 15);

        weapons = new Weapons();
	}

	void OnTriggerEnter(Collider other){
		if (other.transform.tag == "Player") {
			switch (typeBonus){
				case "helth20":
					Actor.GetComponent<Actor>().helth += 20;
				break;
                case "helthReset":
                    Actor.GetComponent<Global>().helthReset += 1;
                break;
                case "armour100":
                if (Actor.GetComponent<Actor>().armour < 100)
                {
                    Actor.GetComponent<Actor>().armourMax = 100;
                    Actor.GetComponent<Actor>().armour = 100;
                }
                break;
                case "armour200":
                if (Actor.GetComponent<Actor>().armour < 200)
                {
                    Actor.GetComponent<Actor>().armourMax = 200;
                    Actor.GetComponent<Actor>().armour = 200;
                }
                break;
                case "armour300":
                if (Actor.GetComponent<Actor>().armour < 300)
                {
                    Actor.GetComponent<Actor>().armourMax = 300;
                    Actor.GetComponent<Actor>().armour = 300;
                }
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


				//Оружие
				case "gun":
                    ZombAll.GetComponent<Survival>().gunBonus = true;
                    Actor.GetComponent<Weapons>().weapons.Add(new Weapon(weapons.indexW++, "gun",  weapons.gun, new Vector3(0, 0, 0)));
				break;
				case "grenade":
                    ZombAll.GetComponent<Survival>().grenadeBonus = true;
                    Actor.GetComponent<Weapons>().weapons.Add(new Weapon(weapons.indexW++, "grenade", weapons.grenade, new Vector3(0, 0, 0)));
				break;
				case "minigun":
                    ZombAll.GetComponent<Survival>().minigunBonus = true;
                    Actor.GetComponent<Weapons>().weapons.Add(new Weapon(weapons.indexW++, "minigun", weapons.minigun, new Vector3(0, 0, 0)));
				break;
				case "rocket":
                    ZombAll.GetComponent<Survival>().rocketBonus = true;
                    Actor.GetComponent<Weapons>().weapons.Add(new Weapon(weapons.indexW++, "rocket", weapons.rocket, new Vector3(0, 0, 0)));
				break;
				case "diskgun":
                    ZombAll.GetComponent<Survival>().diskgunBonus = true;
                    Actor.GetComponent<Weapons>().weapons.Add(new Weapon(weapons.indexW++, "diskgun", weapons.diskgun, new Vector3(0, 0, 0)));
				break;
                case "firegun":
                    ZombAll.GetComponent<Survival>().firegunBonus = true;
                    Actor.GetComponent<Weapons>().weapons.Add(new Weapon(weapons.indexW++, "firegun", weapons.firegun, new Vector3(0, 0, 0)));
                break;
                case "zeusgun":
                    ZombAll.GetComponent<Survival>().zeusgunBonus = true;
                    Actor.GetComponent<Weapons>().weapons.Add(new Weapon(weapons.indexW++, "zeusgun", weapons.zeusgun, new Vector3(0, 0, 0)));
                break;
                case "plasmicgun":
                    ZombAll.GetComponent<Survival>().plasmicgunBonus = true;
                    Actor.GetComponent<Weapons>().weapons.Add(new Weapon(weapons.indexW++, "plasmicgun", weapons.plasmicgun, new Vector3(0, 0, 0)));
                break;
                case "gaussgun":
                    ZombAll.GetComponent<Survival>().gaussgunBonus = true;
                    Actor.GetComponent<Weapons>().weapons.Add(new Weapon(weapons.indexW++, "gaussgun", weapons.gaussgun, new Vector3(0, 0, 0)));
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
