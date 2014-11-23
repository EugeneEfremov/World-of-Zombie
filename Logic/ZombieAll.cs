using UnityEngine;
using System.Collections;

public class ZombieAll : MonoBehaviour {

	public int _newNameZomb = 0, rand, _countTypeZombieInGame = 1, _accountZombMax = 10;
	private Transform Player;
	private bool _bRat = false, _bZombie, _bDog = false, _bSolders = false, _bGrenade = false, _bBigZ = false; //Можно ли создавать зомби
    private string gameMode;
    public Transform rat, zombie1, zombie2, dog, bandit, forester, bigZ, instans; //Объекты
	public int  accountZombNew = 0; //Кол-во зомби в игре на данный момент
    public int accuracyMax, strongMax, speedMax, helthMax; //Сколько раз вываливались бонусы способностей актера
    public float timeZombie = 0.5f, timeZombieNew = 0, timeInGame = 0, instNewWeapTime = 0; //Время до создания зомби, Новое время до создания, Время в игре, время до появления нового оружия

	void Start(){
		Player = GameObject.Find("Actor").transform;
        gameMode = Player.GetComponent<Info>().gameMode.ToString();
	}

	void FixedUpdate(){
		timeInGame += Time.deltaTime;
        instNewWeapTime -= Time.deltaTime;

		//Каких зомби создавать
		if (timeInGame > 0) {
            _bRat = true;
			_accountZombMax = 4;
			if(timeZombie != 0.5f)
                timeZombie = 0.5f;
				}

		if (timeInGame > 10) {
            _bZombie = true;
			_accountZombMax = 8;
			if(timeZombie != 0.4f)
                timeZombie = 0.4f;
			if(_countTypeZombieInGame == 1) 
                _countTypeZombieInGame++;
		}

		if (timeInGame > 15) 
            _accountZombMax = 11;

		if (timeInGame > 20)
            _accountZombMax = 15;

		if (timeInGame > 60) {
            _bDog = true;
			_accountZombMax = 18;
			if(timeZombie != 0.3f)
                timeZombie = 0.3f;
			if(_countTypeZombieInGame == 2)
                _countTypeZombieInGame++;
		}

		if (timeInGame > 180) {
            _bSolders = true;
			_accountZombMax = 20;
			if(timeZombie != 0.2f)
                timeZombie = 0.2f;
			if(_countTypeZombieInGame == 3)
                _countTypeZombieInGame++;
		}

		if (timeInGame > 240) {
            _bGrenade = true;
            _accountZombMax = 19;
			if(_countTypeZombieInGame == 4)
                _countTypeZombieInGame++;
		}

		if (timeInGame > 300) {
            _bBigZ = true;
            _accountZombMax = 30;/*
			if(_countTypeZombieInGame == 5) 
                _countTypeZombieInGame++;*/
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
			return zombie1;
		}
		if (rand == 3 && _bDog) {
			accountZombNew++;
			return dog;
		}
		if (rand == 4 && _bSolders) {
			accountZombNew++;
			return bandit;
		}
		if (rand == 5 && _bGrenade) {
			accountZombNew++;
			return forester;
		}
		if (rand == 6 && _bBigZ) {
			accountZombNew++;
			return bigZ;
		} else {
			accountZombNew++;
			return zombie2;
		}
	}

	private Vector3 Spawn(){
        if (gameMode == "survival"){
            rand = Random.Range(0, 4);
            if (rand == 1) //top
                return Player.position + new Vector3(Random.Range(-20, 20), 1, 25);
            if (rand == 2)//down
                return Player.position + new Vector3(Random.Range(-20, 20), 1, -25);
            if (rand == 3) //left
                return Player.position + new Vector3(-20, 1f, Random.Range(-25, 25));
            else //right
                return Player.position + new Vector3(20, 1f, Random.Range(-25, 25));
	    }
        if (gameMode == "arena")
        {
            rand = Random.Range(0,4);
            if (rand == 1) //top
                return new Vector3(Random.Range(-37,30), 2, 40);
            if (rand == 2)//down
                return new Vector3(Random.Range(-37,30), 2, -20);
            if (rand == 3) //left
                return new Vector3(-37, 2, Random.Range(-20, 40));
            else //right
                return new Vector3(30, 2, Random.Range(-20, 40));
        }
        return new Vector3(); //default
    }
}