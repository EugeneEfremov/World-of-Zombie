using UnityEngine;
using System.Collections;

public class ZombieMove : MonoBehaviour {

	private Transform Player;
	private CharacterController cc;
	private float timeDamage = 1, bloodY = 0.9f; //Переодичность нанесения урона, смещение текстуры

	Vector3 moveDirection = Vector3.zero;

	public string typeZomb;

	public Transform blod;
	public int helth = 100, speed = 3, done = 2;
	
	void Start () {
		Player = GameObject.FindWithTag ("Player").transform;
		cc = GetComponent<CharacterController> ();

		if (typeZomb == "Zombie") {
			speed = 3;
			helth = 100;
			done = 10;
			bloodY = 0.9f;
		}
		if (typeZomb == "Rat") {
			speed = 6;
			helth = 30;
			done = 2;
			bloodY = 0.1f;
		}
		if (typeZomb == "Dog") {
			speed = 7;
			helth = 70;
			done = 12;
			bloodY = 0.1f;
		}
		if (typeZomb == "Solders") {
			speed = 6;
			helth = 150;
			done = 15;
			bloodY = 0.9f;
		}
		if (typeZomb == "Grenade") {
			speed = 6;
			helth = 300;
			done = 20;
			bloodY = 0.9f;
		}
		if (typeZomb == "bZomb") {
			speed = 6;
			helth = 350;
			done = 30;
			bloodY = 0.9f;
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
						Instantiate(blod, new Vector3(transform.position.x, transform.position.y - bloodY, transform.position.z), Quaternion.Euler(new Vector3(0,0,0)));
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
