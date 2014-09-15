using UnityEngine;
using System.Collections;

public class ZombieMove : MonoBehaviour {

	private Transform Player;
	private CharacterController cc;
	private float timeInGame, timeDamage = 1; //Переодичность нанесения урона

	Vector3 moveDirection = Vector3.zero;

	public string typeZomb;

	public Transform blod, helth20, gunBul50, gun, grenage, minigun, rocket, diskgun;
	public int helth = 100, speed = 3, done = 2; //жизнь, скорость, урон
	public AudioClip[] ratA;

	void Start () {
		Player = GameObject.FindWithTag ("Player").transform;
		cc = GetComponent<CharacterController> ();
		timeInGame = GameObject.Find("ZombieLogic").GetComponent<ZombieAll> ().timeInGame;

		if (typeZomb == "Zombie") {
			speed = 2;
			helth = 70;
			done = 6;
		}
		if (typeZomb == "Rat") {
			speed = 6;
			helth = 30;
			done = 2;
		}
		if (typeZomb == "Dog") {
			speed = 7;
			helth = 70;
			done = 10;
		}
		if (typeZomb == "Solders") {
			speed = 6;
			helth = 150;
			done = 15;
		}
		if (typeZomb == "Grenade") {
			speed = 6;
			helth = 300;
			done = 20;
		}
		if (typeZomb == "bZomb") {
			speed = 6;
			helth = 350;
			done = 30;
		}
	}
//Оружие в бонус
	void GunBonus(){
		if (timeInGame > 30 && Player.GetComponent<Weapons>().GBgun == false) {
				Instantiate(gun, transform.position, Quaternion.Euler(0,0,0));
			Player.GetComponent<Weapons>().GBgun = true;
		}
		if (timeInGame > 90 && Player.GetComponent<Weapons>().GBgrenade == false) {
				Instantiate(grenage, transform.position, Quaternion.Euler(0,0,0));
			Player.GetComponent<Weapons>().GBgrenade = true;
		}
		if (timeInGame > 140 && Player.GetComponent<Weapons>().GBminigun == false) {
				Instantiate(minigun, transform.position, Quaternion.Euler(0,0,0));
			Player.GetComponent<Weapons>().GBminigun = true;
		}
		if (timeInGame > 240 && Player.GetComponent<Weapons>().GBrocket == false) {
				Instantiate(rocket, transform.position, Quaternion.Euler(0,0,0));
			Player.GetComponent<Weapons>().GBrocket = true;
		}
		if (timeInGame > 240 && Player.GetComponent<Weapons>().GBdiskgun == false) {
				Instantiate(diskgun, transform.position, Quaternion.Euler(0,0,0));
			Player.GetComponent<Weapons>().GBdiskgun = true;
		}
	}

//Бонусы
	void BonusRandom(){
		int rand = Random.Range (0, 30);
		if (rand <= 3){
			Instantiate(helth20, transform.position, Quaternion.Euler(0,0,0));
		}
		if (rand > 3 && rand <= 6) {
			Instantiate(gunBul50, transform.position, Quaternion.Euler(0,0,0));
		}
	}

	void Update () {
		
				//Поворот
				Vector3 relativePos = Player.position - transform.position;
				Quaternion rotation = Quaternion.LookRotation (relativePos);
				transform.rotation = rotation;
		 
				//Перемещение
				moveDirection = new Vector3 (0, 0, 1);
				moveDirection = transform.TransformDirection (moveDirection);
				moveDirection *= speed;
				moveDirection.y -= 80 * Time.deltaTime;
				cc.Move (moveDirection * Time.deltaTime);
		
				//Смерть 
				if (helth < 1 || transform.position.x < 35 || transform.position.x > 156 || transform.position.z < 25.5f || transform.position.z > 160 || transform.position.y < -2) {
						BonusRandom();
						GunBonus();
						Instantiate(blod, new Vector3(transform.position.x, transform.position.y, transform.position.z), Quaternion.Euler(new Vector3(0,0,0)));
						Destroy (gameObject);
						GameObject.Find("ZombieLogic").GetComponent<ZombieAll>().accountZombNew--;
				}

				RaycastHit zombHit;
				//Урон
				timeDamage -= Time.deltaTime;
				if (Physics.Raycast (gameObject.transform.position, transform.forward, out zombHit, 1.5f)) {
						if (zombHit.transform.tag == "Player") {
							if(timeDamage <= 0){
								Player.GetComponent<Actor> ().helth -= done;
								timeDamage = 1;
							}
						}
				}
		}
}
