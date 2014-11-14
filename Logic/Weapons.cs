using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[System.Serializable]

//В каком виде хранить информацию о доступном оружии в листе
public class Weapon
{
    public Weapon(int index, string currentW,Transform currentWeapObj, Vector3 position)
    {
        this.index = index;
        this.currentW = currentW;
        this.currentWeapObj = currentWeapObj;
        this.position = position;
    }

    public int index;
    public string currentW;
    public Transform currentWeapObj;
    public Vector3 position;
}

public class Weapons : MonoBehaviour{
    //Список оружия
    public List<Weapon> weapons = new List<Weapon>();

    private int _shot, _swithWeapCheckCurent; //Есть ли выстрел, проверка текущего оружия
    private float delayShot; //Пауза выстрела
    private Transform newShellBullet, newBullet, shellSpawn, bulletSpawn, gunSpawn; //новая гильза, новая пуля, спавн гильз, спавн пуль, спавн оружия
    
    //Кол-во доступного оружия, номер текущего оружия
	public int indexW, currentWNum;
    //Сила (кол-во переносимых патрон), точность
    public int strongMax, accuracyMax;
    //Текущее кол-во пуль и максимальное кол-во пуль
    public int gunBullet, gunMax = 100, grenadeBullet, grenadeMax = 100, minigunBullet, minigunMax = 100, rocketBullet, rocketMax = 100, diskgunBullet, diskgunMax = 100, firegunMax = 50, zeusgunMax = 50, plasmicgunMax = 50, gaussgunBullet = 50, gaussgunMax = 100;
    //Уровни оружия
    public int pistolsLvl, gunLvl, grenadeLvl, minigunLvl, rocketLvl, diskgunLvl, firegunLvl, zeusgunLvl, plasmicgunLvl, gaussgunLvl;
    //"Пули" время использования уникального оружия
    public float firegunBullet, zeusgunBullet, plasmicgunBullet;
    //Название объекта текущего оружия
    public string currentW;
    //Активно ли уникально оружие?
    public bool firegunActive = false, zeusgunActive = false, plasmicgunActive = false;

    //Объекты (прицел, гильзы, пули, оружие)
    public Transform goal, shellBullet, bullet, bulletGun, bulletGrenade, bulletRocket, bulletDiskgun, bulletGauss, pistols, gun, grenade, boom, minigun, rocket, diskgun, firegun, zeusgun, plasmicgun, gaussgun;
    //Камера, верх тела, низ тела
    public GameObject cam, topBody, downBody;
    //Звуки выстрелов
    public AudioClip[] pistolsA, gunA, grenadeA;
    //"Скос" для НЕ точности
    public Vector3 NewPositionBulletSpawn;

    void Start()
    {
        strongMax = GetComponent<Global>().strongMax;
        accuracyMax = GetComponent<Global>().accuracyMax;

        cam = GameObject.Find("Camera");
        gunSpawn = GameObject.Find("gunSpawn").transform;
        topBody = GameObject.Find("topBody");
        downBody = GameObject.Find("downBody");

        #region available Weapons

        currentW = "pistols(Clone)";
        currentWNum = 1;
        indexW = 1;

        //Первое оружие всегда пистолет
        weapons.Add(new Weapon(indexW++, "pistols", pistols, new Vector3 (0, 0, 0)));

        //Уровни оружия
        pistolsLvl = PlayerPrefs.GetInt("fx10ab0");
        gunLvl = PlayerPrefs.GetInt("fx10ab1");
        grenadeLvl = PlayerPrefs.GetInt("fx10ab2");
        minigunLvl = PlayerPrefs.GetInt("fx10ab3");
        rocketLvl = PlayerPrefs.GetInt("fx10ab4");
        diskgunLvl = PlayerPrefs.GetInt("fx10ab5");
        firegunLvl = PlayerPrefs.GetInt("fx01e01");
        zeusgunLvl = PlayerPrefs.GetInt("fx01e03");
        plasmicgunLvl = PlayerPrefs.GetInt("fx01e05");
        gaussgunLvl = PlayerPrefs.GetInt("fx01e07");

        //Патроны для оружия
        gunBullet = PlayerPrefs.GetInt("ax90ab1");
        grenadeBullet = PlayerPrefs.GetInt("ax90ab2");
        minigunBullet = PlayerPrefs.GetInt("ax90ab3");
        rocketBullet = PlayerPrefs.GetInt("ax90ab4");
        diskgunBullet = PlayerPrefs.GetInt("ax90ab5");

        //Поставить условие, если режим не выживание
        if (gunLvl > 1)
            weapons.Add(new Weapon(indexW++, "gun", gun, new Vector3(0, 0, 0)));

        if (grenadeLvl > 1)
            weapons.Add(new Weapon(indexW++, "grenade", grenade, new Vector3(0, 0, 0)));

        if (minigunLvl > 1)
            weapons.Add(new Weapon(indexW++, "minigun", minigun, new Vector3(0, 0,0)));

        if (rocketLvl > 1)
            weapons.Add(new Weapon(indexW++, "rocket", rocket, new Vector3(0, 0, 0)));

        if (diskgunLvl > 1)
            weapons.Add(new Weapon(indexW++, "diskgun", diskgun, new Vector3(0, 0, 0)));

        if (firegunLvl > 1)
            weapons.Add(new Weapon(indexW++, "firegun", firegun, new Vector3(0, 0, 0)));

        if (zeusgunLvl > 1)
            weapons.Add(new Weapon(indexW++, "zeusgun", zeusgun, new Vector3(0, 0, 0)));

        if (plasmicgunLvl > 1)
            weapons.Add(new Weapon(indexW++, "plasmicgun", plasmicgun, new Vector3(0, 0, 0)));

        if (gaussgunLvl > 1)
            weapons.Add(new Weapon(indexW++, "gaussgun", gaussgun, new Vector3(0, 0, 0)));
        #endregion

        //Изменение макс. кол-ва патрон
        AlterMaxBullet(strongMax);
    }

    //Изменение макс. кол-ва патрон
    public int AlterMaxBullet(int strongMax)
    {
        gunMax += gunMax * strongMax;
        grenadeMax += grenadeMax * strongMax;
        minigunMax += minigunMax * strongMax;
        rocketMax += rocketMax * strongMax;
        diskgunMax += diskgunMax * strongMax;
        return strongMax;
    }

    //Проверка на макс. кол-во патрон
    public int MaxBullet(int max, int bullet)
    {
        if (bullet > max)
            return max;
        if (bullet < 0)
            return 0;
        return bullet;
    }

    //Смена оружия
    void SelectGun(int currentWNum){
        foreach (Weapon weapon in weapons)
        {
            if (weapon.index == currentWNum)
            {
                SwitchWeapon(weapon.currentWeapObj, weapon.position);
                _swithWeapCheckCurent = currentWNum;
            }
        }
    }

    //Смена оружия ОБЪЕКТ
    void SwitchWeapon(Transform weapon, Vector3 position)
    {
        Destroy(GameObject.Find(currentW));
        Transform newWeap = Instantiate(weapon, new Vector3(gunSpawn.position.x + position.x, gunSpawn.position.y + position.y, gunSpawn.position.z + position.z), Quaternion.Euler(270, topBody.transform.eulerAngles.y + 90, 0)) as Transform;
       
        //Присвоение к объекту актера
        newWeap.parent = topBody.transform;

        //Новое имя текущего оружия
        currentW = newWeap.transform.name;

        //Новая точка спавна патрон
        bulletSpawn = GameObject.Find("bulletSpawn").transform;

        //Новая точка спавна гильз
        shellSpawn = GameObject.Find("shellSpawn").transform;
    }

    //При выстреле передает событие текущему оружию
    void ShotSwitch()
    {
        NewPositionBulletSpawn = new Vector3(bulletSpawn.transform.position.x + Random.Range(-0.1f - accuracyMax, 0.1f + accuracyMax), bulletSpawn.transform.position.y + Random.Range(-0.01f - accuracyMax / 10, 0.01f + accuracyMax / 10), bulletSpawn.transform.position.z);
            switch (currentW)
            {
                case "pistols(Clone)":
                    ShotPistols();
                    break;
                case "gun(Clone)":
                    ShotGun();
                    break;
                case "grenade(Clone)":
                    ShotGrenade();
                    break;
                case "minigun(Clone)":
                    ShotMinigun();
                    break;
                case "rocket(Clone)":
                    ShotRocket();
                    break;
                case "diskgun(Clone)":
                    ShotDiskgun();
                    break;
                case "firegun(Clone)":
                    ShotFiregun(true);
                    break;
                case "zeusgun(Clone)":
                    ShotZeusgun(true);
                    break;
                case "plasmicgun(Clone)":
                    ShotPlasmicgun(true);
                    break;
                case "gaussgun(Clone)":
                    ShotGaussgun();
                    break;
                default:
                    print("default");
                    break;
            }
    }

    //Хор-ки оружия (СОБЫТИЕ ВО ВРЕМЯ ВЫСТРЕЛА)
    #region sepecifications shot weapons
    void ShotPistols(){
		if (delayShot <= 0) {
            //Создание патрона
            newBullet = Instantiate(bullet, NewPositionBulletSpawn, Quaternion.Euler(0, 0, 0)) as Transform;
            newBullet.rigidbody.AddForce( -newBullet.right * 1000);
            //Создание гильзы
            newShellBullet = Instantiate(shellBullet, shellSpawn.position, Quaternion.Euler(0, 0, 0)) as Transform;
            newShellBullet.rigidbody.AddForce( transform.forward * 5);
				}
				if (delayShot <= 0) {
						delayShot = 0.3f;
						GetComponent<AudioSource> ().PlayOneShot (pistolsA [Random.Range (0, pistolsA.Length)]);
				}
	}

	void ShotGun(){
        gunBullet = MaxBullet(gunMax, gunBullet);

        if (delayShot <= 0 && gunBullet > 0)
        {
            //Создание патрона
            newBullet = Instantiate(bulletGun, NewPositionBulletSpawn, Quaternion.Euler(0, 0, 0)) as Transform;
            newBullet.rigidbody.AddForce(newBullet.forward * 1000);
            //Создание гильзы
            newShellBullet = Instantiate(shellBullet, shellSpawn.position, Quaternion.Euler(0, 0, 0)) as Transform;
            newShellBullet.rigidbody.AddForce(transform.right * 5);
		}
		if (delayShot <= 0) {
				gunBullet--;
				delayShot = 0.8f;
		}
	}

	void ShotGrenade(){
        grenadeBullet = MaxBullet(grenadeMax, grenadeBullet);

		if (delayShot <= 0 && grenadeBullet > 0) {
            //Создание патрона
            newBullet = Instantiate(bulletGrenade, NewPositionBulletSpawn, Quaternion.Euler(0,0,0)) as Transform;
            newBullet.rigidbody.AddForce(newBullet.forward * 1000);
            newBullet.GetComponent<Bullet>().type = "Grenade";
            //Создание гильзы
            newShellBullet = Instantiate(shellBullet, shellSpawn.position, Quaternion.Euler(0, 0, 0)) as Transform;
            newShellBullet.rigidbody.AddForce(transform.right * 5);
        }
		if (delayShot <= 0) {
				grenadeBullet--;
				delayShot = 0.8f;
				GetComponent<AudioSource> ().PlayOneShot (grenadeA [0]);
		}
	}

	void ShotMinigun(){
        minigunBullet = MaxBullet(minigunMax, minigunBullet);

        if (delayShot <= 0 && minigunBullet > 0)
        {
            //Создание патрона
            newBullet = Instantiate(bullet, NewPositionBulletSpawn, Quaternion.Euler(0,0,0)) as Transform;
            newBullet.rigidbody.AddForce(newBullet.forward * 1000);
            //Создание гильзы
            newShellBullet = Instantiate(shellBullet, shellSpawn.position, Quaternion.Euler(0, 0, 0)) as Transform;
            newShellBullet.rigidbody.AddForce(transform.right * 5);
		}
		if (delayShot <= 0) {
				minigunBullet--;
				delayShot = 0.1f;
		}
	}

	void ShotRocket(){
        rocketBullet = MaxBullet(rocketMax, rocketBullet);

        if (delayShot <= 0 && rocketBullet > 0)
        {
            //Создание патрона
            newBullet = Instantiate(bulletRocket, NewPositionBulletSpawn, Quaternion.Euler(0, 0, 0)) as Transform;
            newBullet.rigidbody.AddForce(newBullet.forward * 1000);
            newBullet.GetComponent<Bullet>().type = "Rocket";
            //Создание гильзы
            newShellBullet = Instantiate(shellBullet, shellSpawn.position, Quaternion.Euler(0, 0, 0)) as Transform;
            newShellBullet.rigidbody.AddForce(transform.right * 5);
		}
		if (delayShot <= 0) {
			rocketBullet--;
			delayShot = 0.4f;
		}
	}

	void ShotDiskgun(){
        diskgunBullet = MaxBullet(diskgunMax, diskgunBullet);

        if (delayShot <= 0 && diskgunBullet > 0)
        {
            //Создание патрона
            newBullet = Instantiate(bulletDiskgun, NewPositionBulletSpawn, Quaternion.Euler(0, 0, 0)) as Transform;
            newBullet.rigidbody.AddForce(newBullet.forward * 1000);
		}
		if (delayShot <= 0) {
			diskgunBullet--;
			delayShot = 0.5f;
		}
	}

    void ShotFiregun(bool down)
    {
        if (firegunBullet > firegunMax)
            firegunBullet = firegunMax;

        if (firegunBullet > 0 && down)
        {
            firegunActive = true;
            GameObject.Find("FireZone").GetComponent<Collider>().enabled = true;
            GameObject.Find("ParticleFireGun").particleEmitter.emit = true;
        }
        if (firegunBullet < 0 || !down)
        {
            firegunActive = false;
            GameObject.Find("FireZone").GetComponent<Collider>().enabled = false;
            GameObject.Find("ParticleFireGun").particleEmitter.emit = false;
        }
    }

    void ShotZeusgun(bool down)
    {
        if (zeusgunBullet > zeusgunMax)
            zeusgunBullet = zeusgunMax;

        if (zeusgunBullet > 0 && down)
        {
            zeusgunActive = true;
            GameObject.Find("ZeusZone").GetComponent<Collider>().enabled = true;
            GameObject.Find("ZeusGunParticle").particleSystem.enableEmission = true;
        }
        if (zeusgunBullet < 0 || !down)
        {
            zeusgunActive = false;
            GameObject.Find("ZeusZone").GetComponent<Collider>().enabled = false;
            GameObject.Find("ZeusGunParticle").particleSystem.enableEmission = false;
        }
    }

    void ShotPlasmicgun(bool down)
    {
        if (plasmicgunBullet > plasmicgunMax)
            plasmicgunBullet = plasmicgunMax;

        if (plasmicgunBullet > 0 && down)
        {
            plasmicgunActive = true;
            GameObject.Find("PlasmicZone").GetComponent<Collider>().enabled = true;
            GameObject.Find("PlasmicGunParticle").particleSystem.enableEmission = true;
        }
        if (plasmicgunBullet < 0 || !down)
        {
            plasmicgunActive = false;
            GameObject.Find("PlasmicZone").GetComponent<Collider>().enabled = false;
            GameObject.Find("PlasmicGunParticle").particleSystem.enableEmission = false;
        }
    }

    void ShotGaussgun()
    {
        gaussgunBullet = MaxBullet(gaussgunMax, gaussgunBullet);

        if (delayShot <= 0 && gaussgunBullet > 0)
        {
            //Создание патрона
            newBullet = Instantiate(bulletGauss, NewPositionBulletSpawn, Quaternion.Euler(0, 0, 0)) as Transform;
            newBullet.rigidbody.AddForce(newBullet.forward * 1000);
            newBullet.GetComponent<Bullet>().type = "Gauss";
            //Создание гильзы
            newShellBullet = Instantiate(shellBullet, shellSpawn.position, Quaternion.Euler(0, 0, 0)) as Transform;
            newShellBullet.rigidbody.AddForce(transform.right * 5);
        }
        if (delayShot <= 0)
        {
            gaussgunBullet--;
            delayShot = 0.5f;
        }
    }
    #endregion
    //Хор-ки оружия КОНЕЦ

    void FixedUpdate()
    {
        //Есть ли выстрел
        _shot = transform.GetComponent<Controllers>().shot;

        //Смена оружия
        if (_swithWeapCheckCurent != currentWNum)
            SelectGun(currentWNum);
        if (currentWNum > indexW)
            currentWNum = indexW;
        if (currentWNum < 1)
            currentWNum = 1;
        //КОНЕЦ Смена оружия

        //Стрельба
        if (_shot == 1)
            ShotSwitch();

        //Пауза выстрела
        delayShot -= Time.deltaTime;

        //Использование уникального оружия
        #region unique weapons
        //Использование огнемета
        if (firegunActive)
            firegunBullet -= Time.deltaTime;
        if (!firegunActive && firegunBullet < firegunMax)
            firegunBullet += Time.deltaTime;

        //Использование пушки зевса
        if (zeusgunActive)
            zeusgunBullet -= Time.deltaTime;
        if (!zeusgunActive && zeusgunBullet < zeusgunMax)
            zeusgunBullet += Time.deltaTime;

        //Использование плазменной пушки
        if (plasmicgunActive)
            plasmicgunBullet -= Time.deltaTime;
        if (!plasmicgunActive && plasmicgunBullet < plasmicgunMax)
            plasmicgunBullet += Time.deltaTime;

        //Если не ипользуем уникальное оружие
         if (_shot == 0)
          {
             //Действия в зависимости от текущего оружия
             if (currentW == "firegun(Clone)")
                 ShotFiregun(false);
             if (currentW == "zeusgun(Clone)")
                 ShotZeusgun(false);
             if (currentW == "plasmicgun(Clone)")
                 ShotPlasmicgun(false);
          }
        #endregion
    }
}
