﻿using UnityEngine;
using System.Collections;

public class ZombieMove : MonoBehaviour {

	private Transform Player;
	private CharacterController cc;
    private GameObject ZombieAll;
    private float timeInGame, instNewWeapTime, timeDamage = 1, bloodY = 1f; //время в игре, время до созжания нового оружия, переодичность нанесения урона, смещение текстуры
   // public bool InstNewWeap = false;

	Vector3 moveDirection = Vector3.zero;

	public string typeZomb;

	public Transform blod, helth20, gunBul50, gun, grenage, minigun, rocket, diskgun;
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
		int rand = Random.Range (0, 30);
		if (rand <= 3){
			Instantiate(helth20, transform.position, Quaternion.Euler(0,0,0));
		}
		if (rand > 3 && rand <= 6) {
			Instantiate(gunBul50, transform.position, Quaternion.Euler(0,0,0));
		}
	}

    RaycastHit Hit; //Что перед зомби
    RaycastHit HitRight; //Справа от зомби

	void Update () {
        instNewWeapTime = ZombieAll.GetComponent<ZombieAll>().instNewWeapTime;

//Физика движения зомби
        //Луч вперед
        if (Physics.Raycast(gameObject.transform.position, gameObject.transform.forward, out Hit, 100f)){
            if (Hit.transform.tag == "Player") 
                speed = speedMax;
            if (Hit.transform.tag == "Zombie")
                speed = speedMax / 2;
        }
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
                        Instantiate(blod, new Vector3(transform.position.x, transform.position.y - bloodY, transform.position.z), Quaternion.Euler(new Vector3(0, 0, 0)));
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
