using UnityEngine;
using System.Collections;

public class ZombieAll : MonoBehaviour {

	private int _newNameZomb = 0, rand, _countTypeZombieInGame = 1, _accountZombMax = 10;
	private Transform Player;
	private bool _bRat = false, _bZombie, _bDog = false, _bSolders = false, _bGrenade = false, _bBigZ = false; //Можно ли создавать зомби
    public bool magic1;
    public Transform zombie, rat, dog, solders, grenade, bigZ, instans; //Объекты
	public int  accountZombNew = 0; //Кол-во зомби в игре на данный момент
    public int accuracyMax, strongMax, speedMax, helthMax; //Сколько раз вываливались бонусы способностей актера
    public float timeZombie = 0.5f, timeZombieNew = 0, timeInGame = 0, instNewWeapTime = 0; //Время до создания зомби, Новое время до создания, Время в игре, время до появления нового оружия

	void Start(){
		Player = GameObject.FindWithTag ("Player").transform;
	}

	void FixedUpdate(){
		timeInGame += Time.deltaTime;
        instNewWeapTime -= Time.deltaTime;

		//Каких зомби создавать
		if (timeInGame > 0) {
			_accountZombMax = 5;
			if(timeZombie != 0.5f)timeZombie = 0.5f;
			_bRat = true;
				}
		if (timeInGame > 10) {
			_accountZombMax = 9;
			if(timeZombie != 0.4f)timeZombie = 0.4f;
			_bZombie = true;
			if(_countTypeZombieInGame == 1) _countTypeZombieInGame++;
		}
		if (timeInGame > 15) _accountZombMax = 13;
		if (timeInGame > 20) _accountZombMax = 18;
		if (timeInGame > 60) {
			_accountZombMax = 21;
			if(timeZombie != 0.3f)timeZombie = 0.3f;
			_bDog = true;
			if(_countTypeZombieInGame == 2) _countTypeZombieInGame++;
		}
		if (timeInGame > 180) {
			_accountZombMax = 25;
			if(timeZombie != 0.2f)timeZombie = 0.2f;
			_bSolders = true;
			if(_countTypeZombieInGame == 3) _countTypeZombieInGame++;
		}
		if (timeInGame > 240) {
			_bGrenade = true;
			if(_countTypeZombieInGame == 4) _countTypeZombieInGame++;
		}
		if (timeInGame > 300) {
			_bBigZ = true;
			if(_countTypeZombieInGame == 5) _countTypeZombieInGame++;
		}

		timeZombieNew -= Time.deltaTime;

        //Создание зомби
		if (timeZombieNew <= 0 && accountZombNew <= _accountZombMax && Player.GetComponent<Actor>().death != true) {
            instans = Instantiate(Zombie(), Spawn(), Quaternion.Euler(0, 0, 0)) as Transform;
            _newNameZomb++;
            instans.transform.name += _newNameZomb.ToString();
            timeZombieNew = timeZombie;
		}
	}

	private Transform Zombie(){
		rand = Random.Range (0, _countTypeZombieInGame + 1);
		if (rand == 1 && _bRat) {
			accountZombNew++;
			return rat;
		}
		if (rand == 2 && _bZombie) {
			accountZombNew++;
			return zombie;
		}
		if (rand == 3 && _bDog) {
			accountZombNew++;
			return dog;
		}
		if (rand == 4 && _bSolders) {
			accountZombNew++;
			return solders;
		}
		if (rand == 5 && _bGrenade) {
			accountZombNew++;
			return grenade;
		}
		if (rand == 6 && _bBigZ) {
			accountZombNew++;
			return bigZ;
		} else {
			accountZombNew++;
			return zombie;
		}
	}

	private Vector3 Spawn(){
		rand = Random.Range (0, 2);
        if (rand == 1)//лево
            return new Vector3(-17, 2, Random.Range(-15, 15));
        else//право
            return new Vector3(17, 2, Random.Range(-15, 15));
	}
}