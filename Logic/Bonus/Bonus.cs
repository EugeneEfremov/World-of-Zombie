using UnityEngine;
using System.Collections;

public class Bonus : MonoBehaviour {
    private Weapons weaponsClass;
    private GameObject topBody;
    private Animation anim;

	public string typeBonus = ""; //helth20, gunBul50 и т.д.
	public GameObject Actor, ZombAll;
    public Material arm100, arm200, arm300;

	void Start(){
        if (transform.tag != "Weapon")
            anim = transform.GetComponent<Animation>();

		Actor = GameObject.Find ("Actor");
        topBody = GameObject.Find("topBodyAnim");
        ZombAll = GameObject.Find("ZombieLogic");

        weaponsClass = Actor.GetComponent<Weapons>();
	}

    void FixedUpdate()
    {
        if (transform.tag != "Weapon")
            anim.Play();
    }

	void OnTriggerEnter(Collider other){
		if (other.transform.tag == "Player") {
            Actor.GetComponent<Info>().logBonusString += "\n" + typeBonus;

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
                    topBody.renderer.material = arm100;
                }
                break;
                case "armour200":
                if (Actor.GetComponent<Actor>().armour < 200)
                {
                    Actor.GetComponent<Actor>().armourMax = 200;
                    Actor.GetComponent<Actor>().armour = 200;
                    topBody.renderer.material = arm200;
                }
                break;
                case "armour300":
                if (Actor.GetComponent<Actor>().armour < 300)
                {
                    Actor.GetComponent<Actor>().armourMax = 300;
                    Actor.GetComponent<Actor>().armour = 300;
                    topBody.renderer.material = arm300;
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
                    weaponsClass.AvailableWeapons("gun");
				break;
				case "grenade":
                    ZombAll.GetComponent<Survival>().grenadeBonus = true;
                    weaponsClass.AvailableWeapons("grenade");
				break;
				case "minigun":
                    ZombAll.GetComponent<Survival>().minigunBonus = true;
                    weaponsClass.AvailableWeapons("minigun");
				break;
				case "rocket":
                    ZombAll.GetComponent<Survival>().rocketBonus = true;
                    weaponsClass.AvailableWeapons("rocket");
				break;
				case "diskgun":
                    ZombAll.GetComponent<Survival>().diskgunBonus = true;
                    weaponsClass.AvailableWeapons("diskgun");
				break;
                case "firegun":
                    ZombAll.GetComponent<Survival>().firegunBonus = true;
                    weaponsClass.AvailableWeapons("firegun");
                break;
                case "zeusgun":
                    ZombAll.GetComponent<Survival>().zeusgunBonus = true;
                    weaponsClass.AvailableWeapons("zeusgun");
                break;
                case "plasmicgun":
                    ZombAll.GetComponent<Survival>().plasmicgunBonus = true;
                    weaponsClass.AvailableWeapons("plasmicgun");
                break;
                case "gaussgun":
                    ZombAll.GetComponent<Survival>().gaussgunBonus = true;
                    weaponsClass.AvailableWeapons("gaussgun");
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
            transform.parent.GetComponent<Destroying>().destroy = true;
		}
	}
}
