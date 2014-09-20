using UnityEngine;
using System.Collections;

public class ZombieAll : MonoBehaviour {
	
	private int _newNameZomb = 0, rand, _countTypeZombieInGame = 1, _accountZombMax = 10;
	private Vector3 _actorPos;
	private Transform Player;
	private bool _bRat = false, _bZombie, _bDog = false, _bSolders = false, _bGrenade = false, _bBigZ = false; //Можно ли создавать
	public Transform zombie, rat, dog, solders, grenade, bigZ, instans; //Объекты
	public int  accountZombNew = 0; //Кол-во зомби в игре на данный момент
    public float timeZombie = 0.5f, timeZombieNew = 0, timeInGame = 0, instNewWeapTime = 0; //Время до создания зомби, Новое время до создания, Время в игре, время до появления нового оружия

	void Start(){
		Player = GameObject.FindWithTag ("Player").transform;
	}

	void Update(){
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
		if (timeInGame > 15) _accountZombMax = 14;
		if (timeInGame > 20) _accountZombMax = 18;
		if (timeInGame > 60) {
			_accountZombMax = 28;
			if(timeZombie != 0.3f)timeZombie = 0.3f;
			_bDog = true;
			if(_countTypeZombieInGame == 2) _countTypeZombieInGame++;
		}
		if (timeInGame > 180) {
			_accountZombMax = 35;
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

		_actorPos = Player.position;

		timeZombieNew -= Time.deltaTime;

        //Создание зомби
		/*if (timeZombieNew <= 0 && accountZombNew <= _accountZombMax && Player.GetComponent<Actor>().death != true) {
			instans = Instantiate (Zombie(), Spawn(), Quaternion.Euler (0, 0, 0)) as Transform;
			_newNameZomb++;
			instans.transform.name+=_newNameZomb.ToString();
			timeZombieNew = timeZombie;
		}*/
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
		rand = Random.Range (0, 5);
		if (rand == 1)//верх
			return new Vector3 (_actorPos.x + Random.Range (-19, 19), _actorPos.y + 0.1f, _actorPos.z + 28);
		if (rand == 2)//низ
			return new Vector3 (_actorPos.x + Random.Range (-19, 19), _actorPos.y + 0.1f, _actorPos.z - 14);
		if (rand == 3)//право
			return new Vector3 (_actorPos.x + 20, _actorPos.y + 0.1f, _actorPos.z + Random.Range(-13, 25));
		if (rand == 4)//лево
			return new Vector3 (_actorPos.x - 29, _actorPos.y + 0.1f, _actorPos.z + Random.Range (-13, 25));
		else
			return new Vector3 (_actorPos.x + Random.Range (-19, 19), _actorPos.y + 0.1f, _actorPos.z - 14);
	}
}