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

    private int _shot, _swithWeapCheckCurent, _creatingShellBullet = 1; //Есть ли выстрел, проверка текущего оружия
    private float delayShot; //Пауза выстрела
    private bool assignNewSpawn; //Был ли поиск новой точки спавна патрон и гильз?
    private Transform newShellBullet, newBullet, shellSpawn, bulletSpawn, gunSpawn, shotFireEffect, shotFireEffectLight, directionBullet; //новая гильза, новая пуля, спавн гильз, спавн пуль, спавн оружия, оффект огня, объект задающий направление

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
    public Vector3 NewRotationBullet;

    //Добавление оружия актеру
    public void AvailableWeapons(string name)
    {
        if (name == "gun")
        weapons.Add(new Weapon(indexW++, "gun", gun, new Vector3(0, 0, 0)));

        if (name == "grenade")
        weapons.Add(new Weapon(indexW++, "grenade", grenade, new Vector3(0, 0, 0)));

        if (name == "minigun")
        weapons.Add(new Weapon(indexW++, "minigun", minigun, new Vector3(0, 0, 0)));

        if (name == "rocket")
        weapons.Add(new Weapon(indexW++, "rocket", rocket, new Vector3(-0.05f, -0.3f, 0)));

        if (name == "diskgun")
        weapons.Add(new Weapon(indexW++, "diskgun", diskgun, new Vector3(0, 0, 0)));

        if (name == "firegun")
        weapons.Add(new Weapon(indexW++, "firegun", firegun, new Vector3(0, 0, 0)));

        if (name == "zeusgun")
        weapons.Add(new Weapon(indexW++, "zeusgun", zeusgun, new Vector3(0, 0, -0.2f)));

        if (name == "plasmicgun")
        weapons.Add(new Weapon(indexW++, "plasmicgun", plasmicgun, new Vector3(0, 0, 0)));

        if (name == "gaussgun")
        weapons.Add(new Weapon(indexW++, "gaussgun", gaussgun, new Vector3(0, 0, 0)));
    }

    void Start()
    {
        strongMax = GetComponent<Global>().strongMax;
        accuracyMax = GetComponent<Global>().accuracyMax;

        cam = GameObject.Find("Camera");
        gunSpawn = GameObject.Find("gunSpawn").transform;
        topBody = GameObject.Find("topBody");
        downBody = GameObject.Find("downBody");
        directionBullet = GameObject.Find("directionBullet").transform;

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

        _creatingShellBullet = PlayerPrefs.GetInt("shellBullet");

        if (transform.GetComponent<Info>().gameMode != Info.GameMode.survival)
        {
            if (gunLvl > 1)
                AvailableWeapons("gun");

            if (grenadeLvl > 1)
                AvailableWeapons("grenade");

            if (minigunLvl > 1)
                AvailableWeapons("minigun");

            if (rocketLvl > 1)
                AvailableWeapons("rocket");

            if (diskgunLvl > 1)
                AvailableWeapons("diskgun");

            if (firegunLvl > 1)
                AvailableWeapons("firegun");

            if (zeusgunLvl > 1)
                AvailableWeapons("zeusgun");

            if (plasmicgunLvl > 1)
                AvailableWeapons("plasmicgun");

            if (gaussgunLvl > 1)
                AvailableWeapons("gaussgun");
        }
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
        _shot = 0;
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
        assignNewSpawn = false;

        Destroy(GameObject.Find(currentW));
        Transform newWeap = Instantiate(weapon, new Vector3(gunSpawn.position.x + position.x, gunSpawn.position.y + position.y, gunSpawn.position.z + position.z), Quaternion.Euler(270, topBody.transform.eulerAngles.y + 90, 0)) as Transform;
       
        //Присвоение к объекту актера
        newWeap.parent = topBody.transform;

        //Новое имя текущего оружия
        currentW = newWeap.transform.name;
    }

    //При выстреле передает событие текущему оружию
    void ShotSwitch()
    {
        NewRotationBullet = new Vector3(Random.Range(-8 * accuracyMax, 8 * accuracyMax), 0, 0);
        directionBullet.eulerAngles = NewRotationBullet;    
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
            newBullet = Instantiate(bullet, bulletSpawn.position, Quaternion.Euler(0, 0, 0)) as Transform;
            newBullet.rigidbody.AddForce(bulletSpawn.TransformDirection(directionBullet.up) * 800);
            //Создание гильзы
            if (_creatingShellBullet != 0)
            {
                newShellBullet = Instantiate(shellBullet, shellSpawn.position, Quaternion.Euler(0, 0, 0)) as Transform;
                newShellBullet.rigidbody.AddForce(transform.forward * 1);
            }
				}
				if (delayShot <= 0) {
						delayShot = 0.3f;
						GetComponent<AudioSource> ().PlayOneShot (pistolsA [Random.Range (0, pistolsA.Length)]);
				}

                //Эффект выстрела
                if (delayShot > 0.2f)
                {
                    shotFireEffect.transform.renderer.enabled = true;
                    shotFireEffectLight.transform.light.enabled = true;
                }
                if (delayShot <= 0.2f)
                {
                    shotFireEffect.transform.renderer.enabled = false;
                    shotFireEffectLight.transform.light.enabled = false;
                }
	}

	void ShotGun(){
        gunBullet = MaxBullet(gunMax, gunBullet);

        if (delayShot <= 0 && gunBullet > 0)
        {
            //Создание патрона
            newBullet = Instantiate(bulletGun, bulletSpawn.position, Quaternion.Euler(0, 0, 0)) as Transform;
            newBullet.rigidbody.AddForce(bulletSpawn.TransformDirection(directionBullet.up) * 1400);
            //Создание гильзы
            if (_creatingShellBullet != 0)
            {
                newShellBullet = Instantiate(shellBullet, shellSpawn.position, Quaternion.Euler(0, 0, 0)) as Transform;
                newShellBullet.rigidbody.AddForce(transform.right * 10);
            }
		}
		if (delayShot <= 0) {
				gunBullet--;
				delayShot = 0.8f;
		}

        //Эффект выстрела
        if (delayShot > 0.6f && gunBullet > 0)
        {
            shotFireEffect.transform.renderer.enabled = true;
            shotFireEffectLight.transform.light.enabled = true;
        }
        if (delayShot <= 0.6f)
        {
            shotFireEffect.transform.renderer.enabled = false;
            shotFireEffectLight.transform.light.enabled = false;
        }
	}

	void ShotGrenade(){
        grenadeBullet = MaxBullet(grenadeMax, grenadeBullet);

		if (delayShot <= 0 && grenadeBullet > 0) {
            //Создание патрона
            newBullet = Instantiate(bulletGrenade, bulletSpawn.position, Quaternion.Euler(0, 0, 0)) as Transform;
            newBullet.rigidbody.AddForce(bulletSpawn.TransformDirection(directionBullet.up) * 1000);
            newBullet.GetComponent<Bullet>().type = "Grenade";
        }
		if (delayShot <= 0) {
				grenadeBullet--;
				delayShot = 0.8f;
				GetComponent<AudioSource> ().PlayOneShot (grenadeA [0]);
		}

        //Эффект выстрела
        if (delayShot > 0.6f && grenadeBullet > 0)
        {
            shotFireEffect.transform.renderer.enabled = true;
            shotFireEffectLight.transform.light.enabled = true;
        }
        if (delayShot <= 0.6f)
        {
            shotFireEffect.transform.renderer.enabled = false;
            shotFireEffectLight.transform.light.enabled = false;
        }
	}

	void ShotMinigun(){
        minigunBullet = MaxBullet(minigunMax, minigunBullet);

        if (delayShot <= 0 && minigunBullet > 0)
        {
            //Создание патрона
            newBullet = Instantiate(bullet, bulletSpawn.position, Quaternion.Euler(0, 0, 0)) as Transform;
            newBullet.rigidbody.AddForce(bulletSpawn.TransformDirection(directionBullet.up) * 1000);
            //Создание гильзы
            if (_creatingShellBullet != 0)
            {
                newShellBullet = Instantiate(shellBullet, shellSpawn.position, Quaternion.Euler(0, 0, 0)) as Transform;
                newShellBullet.rigidbody.AddForce(transform.right * 15);
            }
		}
		if (delayShot <= 0) {
				minigunBullet--;
				delayShot = 0.3f;
		}

        //Эффект выстрела
        if (delayShot > 0.15 && minigunBullet > 0)
        {
            shotFireEffect.transform.renderer.enabled = true;
            shotFireEffectLight.transform.light.enabled = true;
        }
        if (delayShot <= 0.15f)
        {
            shotFireEffect.transform.renderer.enabled = false;
            shotFireEffectLight.transform.light.enabled = false;
        }
	}

	void ShotRocket(){
        rocketBullet = MaxBullet(rocketMax, rocketBullet);

        if (delayShot <= 0 && rocketBullet > 0)
        {
            //Создание патрона
            newBullet = Instantiate(bulletRocket, bulletSpawn.position, Quaternion.Euler(0, 0, 0)) as Transform;
            newBullet.rigidbody.AddForce(bulletSpawn.TransformDirection(directionBullet.up) * 1000);
            newBullet.GetComponent<Bullet>().type = "Rocket";
		}
		if (delayShot <= 0) {
			rocketBullet--;
			delayShot = 0.4f;
		}

        //Эффект выстрела
        if (delayShot > 0.2 && rocketBullet > 0)
        {
            shotFireEffect.transform.renderer.enabled = true;
            shotFireEffectLight.transform.light.enabled = true;
        }
        if (delayShot <= 0.2f)
        {
            shotFireEffect.transform.renderer.enabled = false;
            shotFireEffectLight.transform.light.enabled = false;
        }
	}

	void ShotDiskgun(){
        diskgunBullet = MaxBullet(diskgunMax, diskgunBullet);

        if (delayShot <= 0 && diskgunBullet > 0)
        {
            //Создание патрона
            newBullet = Instantiate(bulletDiskgun, bulletSpawn.position, Quaternion.Euler(90, 0, 0)) as Transform;
            newBullet.rigidbody.AddForce(bulletSpawn.TransformDirection(directionBullet.up) * 1000);
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

        if (firegunBullet > 0 && down && _shot == 1)
        {
            firegunActive = true;
            GameObject.Find("FireZone").GetComponent<PowerGun>().shot = true;
            GameObject.Find("ParticleFireGun").particleSystem.enableEmission = true;
        }
        if (firegunBullet < 0 || !down)
        {
            firegunActive = false;
            GameObject.Find("FireZone").GetComponent<PowerGun>().shot = false;
            GameObject.Find("ParticleFireGun").particleSystem.enableEmission = false;
        }
    }

    void ShotZeusgun(bool down)
    {
        if (zeusgunBullet > zeusgunMax)
            zeusgunBullet = zeusgunMax;

        if (zeusgunBullet > 0 && down)
        {
            zeusgunActive = true;
            GameObject.Find("ZeusZone").GetComponent<PowerGun>().shot = true;
            GameObject.Find("ZeusGunParticle").particleSystem.enableEmission = true;
            GameObject.Find("ZeusGunParticle1").particleSystem.enableEmission = true;
        }
        if (zeusgunBullet < 0 || !down)
        {
            zeusgunActive = false;
            GameObject.Find("ZeusZone").GetComponent<PowerGun>().shot = false;
            GameObject.Find("ZeusGunParticle").particleSystem.enableEmission = false;
            GameObject.Find("ZeusGunParticle1").particleSystem.enableEmission = false;
        }
    }

    void ShotPlasmicgun(bool down)
    {
        if (plasmicgunBullet > plasmicgunMax)
            plasmicgunBullet = plasmicgunMax;

        if (plasmicgunBullet > 0 && down)
        {
            plasmicgunActive = true;
            GameObject.Find("PlasmicZone").GetComponent<PowerGun>().shot = true;
            GameObject.Find("PlasmicGunParticle").particleSystem.enableEmission = true;
        }
        if (plasmicgunBullet < 0 || !down)
        {
            plasmicgunActive = false;
            GameObject.Find("PlasmicZone").GetComponent<PowerGun>().shot = false;
            GameObject.Find("PlasmicGunParticle").particleSystem.enableEmission = false;
        }
    }

    void ShotGaussgun()
    {
        gaussgunBullet = MaxBullet(gaussgunMax, gaussgunBullet);

        if (delayShot <= 0 && gaussgunBullet > 0)
        {
            //Создание патрона
            newBullet = Instantiate(bulletGauss, bulletSpawn.position, Quaternion.Euler(0, 0, 0)) as Transform;
            newBullet.rigidbody.AddForce(bulletSpawn.TransformDirection(directionBullet.up) * 1000);
            newBullet.GetComponent<Bullet>().type = "Gauss";
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
        {
            if (!assignNewSpawn)
            {
                shotFireEffect = GameObject.Find("shotFireEffect").transform;
                shotFireEffectLight = GameObject.Find("shotFireEffectLight").transform;

                //Новая точка спавна патрон
                bulletSpawn = GameObject.Find("bulletSpawn").transform;

                //Новая точка спавна гильз
                shellSpawn = GameObject.Find("shellSpawn").transform;

                assignNewSpawn = true;
            }
            ShotSwitch();
        }

        if (_shot == 0)
        {
            if (assignNewSpawn)
            {
                shotFireEffect.transform.renderer.enabled = false;
                shotFireEffectLight.transform.light.enabled = false;
            }

            //Действия в зависимости от текущего оружия
            if (currentW == "firegun(Clone)")
                ShotFiregun(false);
            if (currentW == "zeusgun(Clone)")
                ShotZeusgun(false);
            if (currentW == "plasmicgun(Clone)")
                ShotPlasmicgun(false);
        }

        //Пауза выстрела
        delayShot -= Time.deltaTime;

        //Использование уникального оружия
        #region unique weapons
        //Использование огнемета
        if (firegunActive)
            firegunBullet -= Time.deltaTime / 1.5f;

        //Использование пушки зевса
        if (zeusgunActive)
            zeusgunBullet -= Time.deltaTime / 1.5f ;

        //Использование плазменной пушки
        if (plasmicgunActive)
            plasmicgunBullet -= Time.deltaTime / 1.5f;
        #endregion
    }
}
