using UnityEngine;
using System.Collections;

public class ZombieMove : MonoBehaviour {

	private Transform Player;
	private CharacterController cc;
    private GameObject ZombieAll;
    private float timeInGame, instNewWeapTime, timeDamage = 1.2f, bloodY = 1f; //время в игре, время до созжания нового оружия, переодичность нанесения урона, смещение текстуры
   // public bool InstNewWeap = false;

	Vector3 moveDirection = Vector3.zero;

	public string typeZomb;
    public bool magic1;
    public Transform blod, helth20, armour100, gunBul50, grenadeBul50, minigunBul500, rocketBul50, diskgunBul50, gun, grenage, minigun, rocket, diskgun, accuracyMaxBonus, strongMaxBonus, speedMaxBonus, helthMaxBonus;
	public int helth = 100, done = 2; //жизнь, урон
    public float speed = 3, speedMax = 3;
	public AudioClip[] ratA;

	void Start () {
		Player = GameObject.FindWithTag ("Player").transform;
		cc = GetComponent<CharacterController> ();
		timeInGame = GameObject.Find("ZombieLogic").GetComponent<ZombieAll> ().timeInGame;
        ZombieAll = GameObject.Find("ZombieLogic");

		if (typeZomb == "Zombie") {
            speed = 2;
            speedMax = 2.5f;
			helth = 70;
			done = 6;
            bloodY = 0.9f;
		}
		if (typeZomb == "Rat") {
			speed = 6;
            speedMax = 6;
			helth = 30;
			done = 2;
            bloodY = 0.1f;
		}
		if (typeZomb == "Dog") {
			speed = 7;
            speedMax = 7;
			helth = 70;
			done = 10;
            bloodY = 0.1f;
		}
		if (typeZomb == "Solders") {
			speed = 5.5f;
            speedMax = 5.5f;
			helth = 150;
			done = 15;
            bloodY = 0.9f;
		}
		if (typeZomb == "Grenade") {
			speed = 3;
            speedMax = 3;
			helth = 300;
			done = 20;
            bloodY = 0.9f;
		}
		if (typeZomb == "bZomb") {
			speed = 4;
            speedMax = 4;
			helth = 350;
			done = 30;
            bloodY = 0.9f;
		}
	}

//Оружие в бонус
	void GunBonus(){
		if (timeInGame > 30 && Player.GetComponent<Weapons>().GBgun == false && instNewWeapTime <= 0) {
				Instantiate(gun, transform.position, Quaternion.Euler(0,0,0));
                ZombieAll.GetComponent<ZombieAll>().instNewWeapTime = 14;
		}
        if (timeInGame > 90 && Player.GetComponent<Weapons>().GBgrenade == false && instNewWeapTime <= 0)
        {
				Instantiate(grenage, transform.position, Quaternion.Euler(0,0,0));
                ZombieAll.GetComponent<ZombieAll>().instNewWeapTime = 14;
		}
        if (timeInGame > 140 && Player.GetComponent<Weapons>().GBminigun == false && instNewWeapTime <= 0)
        {
				Instantiate(minigun, transform.position, Quaternion.Euler(0,0,0));
                ZombieAll.GetComponent<ZombieAll>().instNewWeapTime = 14;
		}
        if (timeInGame > 240 && Player.GetComponent<Weapons>().GBrocket == false && instNewWeapTime <= 0)
        {
				Instantiate(rocket, transform.position, Quaternion.Euler(0,0,0));
                ZombieAll.GetComponent<ZombieAll>().instNewWeapTime = 14;
		}
        if (timeInGame > 240 && Player.GetComponent<Weapons>().GBdiskgun == false && instNewWeapTime <= 0)
        {
				Instantiate(diskgun, transform.position, Quaternion.Euler(0,0,0));
                ZombieAll.GetComponent<ZombieAll>().instNewWeapTime = 14;
		}
	}

//Бонусы
	void BonusRandom(){
		int rand = Random.Range (0, 300);
		if (rand <= 10){
			Instantiate(helth20, transform.position, Quaternion.Euler(0,0,0));
		}
		if (rand > 10 && rand <= 15) {
			Instantiate(gunBul50, transform.position, Quaternion.Euler(0,0,0));
		}
        if (rand > 30 && rand <= 35)
        {
            Instantiate(grenadeBul50, transform.position, Quaternion.Euler(0, 0, 0));
        }
        if (rand > 50 && rand <= 55)
        {
            Instantiate(minigunBul500, transform.position, Quaternion.Euler(0, 0, 0));
        }
        if (rand > 70 && rand <= 75)
        {
            Instantiate(rocketBul50, transform.position, Quaternion.Euler(0, 0, 0));
        }
        if (rand > 90 && rand <= 95)
        {
            Instantiate(diskgunBul50, transform.position, Quaternion.Euler(0, 0, 0));
        }
        if (rand > 110 && rand <= 115)
        {
            Instantiate(armour100, transform.position, Quaternion.Euler(0, 0, 0));
        }

        //Способности
        if (rand > 130 && rand <= 135 && ZombieAll.GetComponent<ZombieAll>().accuracyMax < 5)
        {
            Instantiate(accuracyMaxBonus, transform.position, Quaternion.Euler(0, 0, 0));
            ZombieAll.GetComponent<ZombieAll>().accuracyMax += 1;
        }
        if (rand > 150 && rand <= 155 && ZombieAll.GetComponent<ZombieAll>().strongMax < 5)
        {
            Instantiate(strongMaxBonus, transform.position, Quaternion.Euler(0, 0, 0));
            ZombieAll.GetComponent<ZombieAll>().strongMax += 1;
        }
        if (rand > 170 && rand <= 175 && ZombieAll.GetComponent<ZombieAll>().speedMax < 5)
        {
            Instantiate(speedMaxBonus, transform.position, Quaternion.Euler(0, 0, 0));
            ZombieAll.GetComponent<ZombieAll>().speedMax += 1;
        }
        if (rand > 190 && rand <= 195 && ZombieAll.GetComponent<ZombieAll>().helthMax < 5)
        {
            Instantiate(helthMaxBonus, transform.position, Quaternion.Euler(0, 0, 0));
            ZombieAll.GetComponent<ZombieAll>().helthMax += 1;
        }
	}

    RaycastHit Hit; //Что перед зомби
    RaycastHit HitRight; //Справа от зомби

	void FixedUpdate () {
        instNewWeapTime = ZombieAll.GetComponent<ZombieAll>().instNewWeapTime;
        magic1 = ZombieAll.GetComponent<ZombieAll>().magic1;

//Физика движения зомби
        //Луч вперед
        if (Physics.Raycast(gameObject.transform.position, gameObject.transform.forward, out Hit, 100f)){
            if (Hit.transform.tag == "Player") 
                speed = speedMax;
            if (Hit.transform.tag == "Zombie")
                speed = speedMax / 2;
            if (magic1)
                speed /= 2;
        }
				//Поворот
				Vector3 relativePos = Player.position - transform.position;
				Quaternion rotation = Quaternion.LookRotation (relativePos);
				transform.rotation = rotation;
		 
				//Перемещение
				moveDirection = new Vector3 (0, 0, 1);
				moveDirection = transform.TransformDirection (moveDirection);
				moveDirection *= speed;
				moveDirection.y -= 150 * Time.deltaTime;
				cc.Move (moveDirection * Time.deltaTime);
		
				//Смерть 
				if (helth < 1) {
						BonusRandom();
						GunBonus();
                        Instantiate(blod, new Vector3(transform.position.x, transform.position.y - bloodY, transform.position.z), Quaternion.Euler(new Vector3(0, 0, 0)));
						Destroy (gameObject);
						ZombieAll.GetComponent<ZombieAll>().accountZombNew--;
				}

				RaycastHit zombHit;
				//Урон
				timeDamage -= Time.deltaTime;
				if (Physics.Raycast (gameObject.transform.position, transform.forward, out zombHit, 1.5f)) {
						if (zombHit.transform.tag == "Player") {
							if(timeDamage <= 0){
                                if (Player.GetComponent<Actor>().armour > 0)
                                {
                                    Player.GetComponent<Actor>().helth -= (int)(done / 2f);
                                    Player.GetComponent<Actor>().armour -= (int)(done / 2f);
                                }
                                if (Player.GetComponent<Actor>().armour <= 0)
                                    Player.GetComponent<Actor>().helth -= done;

								timeDamage = 1.2f;
							}
						}
				}
		}
}
