using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public class Weapon
{
    public Weapon(int index, Transform currentW)
    {
        this.index = index;
        this.currentW = currentW;
    }

    public int index;
    public Transform currentW;
}

public class Weapons : MonoBehaviour{
    public List<Weapon> weapons = new List<Weapon>();

	private float delayShot; //Пауза выстрела
    private Transform newShellBullet, newBullet;

	public int indexW ,currentWNum, strongMax, accuracyMax, gunBullet, gunMax = 100, grenadeBullet, grenadeMax = 100, minigunBullet, minigunMax = 100, rocketBullet, rocketMax = 100, diskgunBullet, diskgunMax = 100, firegunMax = 50, zeusgunMax = 50, plasmicgunMax = 50, gaussgunBullet = 50, gaussgunMax = 100; //Патроны, максимальное кол-во
    public int pistolsLvl, gunLvl, grenadeLvl, minigunLvl, rocketLvl, diskgunLvl, firegunLvl, zeusgunLvl, plasmicgunLvl, gaussgunLvl; //Level of Weapons
    public float firegunBullet, zeusgunBullet, plasmicgunBullet;
    public GameObject cam, topBody, downBody;
	public bool buttonSwitchOn = false, pistolsC = true, gunC = false, grenadeC = false, minigunC = false, rocketC = false, diskgunC = false, firegunC = false, firegunActive = false, zeusgunC = false, zeusgunActive = false, plasmicgunC = false, plasmicgunActive = false, gaussgunC = false; //Наличие
	public string currentW;
	public Transform shellBullet, bullet, bulletGun, bulletGrenade, bulletRocket, bulletDiskgun, pistols, gun, grenade, boom, minigun, rocket, diskgun, firegun, zeusgun, plasmicgun, gaussgun; //Объекты
	public AudioClip[] pistolsA, gunA, grenadeA; //Звуки выстрелов
	public bool GBgun = false, GBgrenade = false, GBminigun = false, GBrocket = false, GBdiskgun = false, GBfiregun = false, GBzeusgun = false, GBplasmicgun = false, GBgaussgun = false; //Открыто ли оружие
    public Vector3 NewPositionGoal;

    void Start()
    {

        strongMax = GetComponent<Global>().strongMax;
        accuracyMax = GetComponent<Global>().accuracyMax;

        topBody = GameObject.Find("topBody");
        downBody = GameObject.Find("downBody");
        cam = GameObject.Find("Camera");

        #region available Weapons
        currentW = "pistols(Clone)";
        currentWNum = 1;
        indexW = 1;

        weapons.Add(new Weapon(indexW++, pistols));

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

        gunBullet = PlayerPrefs.GetInt("ax90ab1");
        grenadeBullet = PlayerPrefs.GetInt("ax90ab2");
        minigunBullet = PlayerPrefs.GetInt("ax90ab3");
        rocketBullet = PlayerPrefs.GetInt("ax90ab4");
        diskgunBullet = PlayerPrefs.GetInt("ax90ab5");

        //Поставить условие, если режим не выживание
        if (gunLvl > 1)
        {
            gunC = true;
            weapons.Add(new Weapon(indexW++, gun));
        }
        if (grenadeLvl > 1)
        {
            grenadeC = true;
            weapons.Add(new Weapon(indexW++, grenade));
        }
        if (minigunLvl > 1)
        {
            minigunC = true;
            weapons.Add(new Weapon(indexW++, minigun));
        }
        if (rocketLvl > 1)
        {
            rocketC = true;
            weapons.Add(new Weapon(indexW++, rocket));
        }
        if (diskgunLvl > 1)
        {
            diskgunC = true;
            weapons.Add(new Weapon(indexW++, diskgun));
        }
        if (firegunLvl > 1)
        {
            firegunC = true;
            weapons.Add(new Weapon(indexW++, firegun));
        }
        if (zeusgunLvl > 1)
        {
            zeusgunC = true;
            weapons.Add(new Weapon(indexW++, zeusgun));
        }
        if (plasmicgunLvl > 1)
        {
            plasmicgunC = true;
            weapons.Add(new Weapon(indexW++, plasmicgun));
        }
        if (gaussgunLvl > 1)
        {
            gaussgunC = true;
            weapons.Add(new Weapon(indexW++, gaussgun));
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

    void SelectGun(int currentWNum){
        foreach (Weapon weapon in weapons)
        {
            if (weapon.index == currentWNum)
                SwitchWeapon(weapon.currentW);
        }
    }

	void SwitchWeapon(Transform weapon){
		Destroy (GameObject.Find(currentW));
		Transform newWeap = Instantiate (weapon, transform.position, Quaternion.Euler(0,0,0)) as Transform;
        newWeap.GetComponent<Bonus>().enabled = false;
        newWeap.GetComponent<Collider>().enabled = false;
        newWeap.transform.rotation = topBody.transform.rotation;
		newWeap.parent = topBody.transform; //Присвоение к объекту актера
		currentW = newWeap.transform.name;
	}
    
	RaycastHit hitObj; //Куда попал патрон
	Ray camRay; //Луч выпускаемый из прицела
	RaycastHit goal; //Куда смотрит прицел

//Хор-ки оружия (СОБЫТИЕ ВО ВРЕМЯ ВЫСТРЕЛА)
	void ShotPistols(){
		if (delayShot <= 0) {
            newBullet = Instantiate(bullet, topBody.transform.position, Quaternion.Euler(0,0,0)) as Transform;
            newBullet.rigidbody.AddForce(topBody.transform.forward * 1000);
            newShellBullet = Instantiate(shellBullet, new Vector3(topBody.transform.position.x + 0.5f, topBody.transform.position.y - 1, topBody.transform.position.z), Quaternion.Euler(0, 0, 0)) as Transform;
            newShellBullet.rigidbody.AddForce(transform.right * 5);
                    if (hitObj.transform.tag == "Zombie")
                    {
                        GameObject.Find(hitObj.transform.name).GetComponent<ZombieMove>().helth -= 30;
                        transform.GetComponent<Actor>().count += 30;
                    }
                    if (hitObj.transform.tag == "Barel")
                    {
                        hitObj.transform.GetComponent<Barel>().helth -= 100;
                        transform.GetComponent<Actor>().count += 30;
                    }
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
            newBullet = Instantiate(bulletGun, topBody.transform.position, Quaternion.Euler(0,0,0)) as Transform;
            newBullet.rigidbody.AddForce(topBody.transform.forward * 1000);
            if (hitObj.transform.tag == "Zombie")
            {
                GameObject.Find(hitObj.transform.name).GetComponent<ZombieMove>().helth -= 60;
                transform.GetComponent<Actor>().count += 60;
            }
            if (hitObj.transform.tag == "Barel")
            {
                hitObj.transform.GetComponent<Barel>().helth -= 100;
                transform.GetComponent<Actor>().count += 30;
            }
		}
		if (delayShot <= 0) {
				gunBullet--;
				delayShot = 0.8f;
		}
	}

	void ShotGrenade(){
        grenadeBullet = MaxBullet(grenadeMax, grenadeBullet);

		if (delayShot <= 0 && grenadeBullet > 0) {
            newBullet = Instantiate(bulletGrenade, topBody.transform.position, Quaternion.Euler(0,0,0)) as Transform;
            newBullet.rigidbody.AddForce(topBody.transform.forward * 1000);
            newBullet.GetComponent<Bullet>().position = hitObj.point;
            newBullet.GetComponent<Bullet>().type = "Grenade";
			//Instantiate(boom, hitObj.point, Quaternion.Euler(0,0,0));
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
            newBullet = Instantiate(bullet, topBody.transform.position, Quaternion.Euler(0,0,0)) as Transform;
            newBullet.rigidbody.AddForce(topBody.transform.forward * 1000);
            if (hitObj.transform.tag == "Zombie")
            {
			    GameObject.Find (hitObj.transform.name).GetComponent<ZombieMove> ().helth -= 30;
			    transform.GetComponent<Actor> ().count += 25;
            }
            if (hitObj.transform.tag == "Barel")
            {
                hitObj.transform.GetComponent<Barel>().helth -= 100;
                transform.GetComponent<Actor>().count += 30;
            }
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
            newBullet = Instantiate(bulletRocket, topBody.transform.position, Quaternion.Euler(0,0,0)) as Transform;
            newBullet.rigidbody.AddForce(topBody.transform.forward * 1000);
            newBullet.GetComponent<Bullet>().position = hitObj.point;
            newBullet.GetComponent<Bullet>().type = "Rocket";
			//Instantiate(boom, hitObj.point, Quaternion.Euler(0,0,0));
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
            newBullet = Instantiate(bulletDiskgun, topBody.transform.position, Quaternion.Euler(0, 0, 0)) as Transform;
            newBullet.rigidbody.AddForce(topBody.transform.forward * 1000);
            if (hitObj.transform.tag == "Zombie")
            {
                GameObject.Find(hitObj.transform.name).GetComponent<ZombieMove>().helth -= 150;
                transform.GetComponent<Actor>().count += 150;
            }
            if (hitObj.transform.tag == "Barel")
            {
                hitObj.transform.GetComponent<Barel>().helth -= 100;
                transform.GetComponent<Actor>().count += 30;
            }
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

        if (delayShot <= 0 && hitObj.transform.tag == "Zombie" && gaussgunBullet > 0)
        {
            GameObject.Find(hitObj.transform.name).GetComponent<ZombieMove>().helth -= 100;
            transform.GetComponent<Actor>().count += 100;
        }
        if (hitObj.transform.tag == "Barel")
        {
            hitObj.transform.GetComponent<Barel>().helth -= 100;
            transform.GetComponent<Actor>().count += 30;
        }
        if (delayShot <= 0)
        {
            gaussgunBullet--;
            delayShot = 0.5f;
        }
    }
//Хор-ки оружия КОНЕЦ

	void FixedUpdate() {

        //Смена оружия
        SelectGun(currentWNum);
        if (currentWNum > indexW)
            currentWNum = indexW;
        if (currentWNum < 1)
            currentWNum = 1;
        //КОНЕЦ Смена оружия

		delayShot -= Time.deltaTime;

        if (firegunActive)
            firegunBullet -= Time.deltaTime;
        if (!firegunActive && firegunBullet < firegunMax)
            firegunBullet += Time.deltaTime;

        if (zeusgunActive)
            zeusgunBullet -= Time.deltaTime;
        if (!zeusgunActive && zeusgunBullet < zeusgunMax)
            zeusgunBullet += Time.deltaTime;

        if (plasmicgunActive)
            plasmicgunBullet -= Time.deltaTime;
        if (!plasmicgunActive && plasmicgunBullet < plasmicgunMax)
            plasmicgunBullet += Time.deltaTime;

        camRay = cam.camera.ScreenPointToRay (Input.mousePosition); //Позиция прицела
				if (Physics.Raycast (cam.transform.position, camRay.direction, out goal, 100f)) {//Куда смотрит прицел
						//Debug.DrawLine (cam.transform.position, goal.point, Color.red);

                        //Вращение верхней части тела за прицелом
                    /*
                         Vector3 relativePos = goal.point - topBody.transform.position;
                         Quaternion rotation = Quaternion.LookRotation(relativePos);
                         topBody.transform.rotation = rotation;
                     */
                    topBody.transform.eulerAngles = new Vector3 (0, transform.GetComponent<Controllers>().rotationCC, 0);

                    //Поворот нижней части тела
                         //print(topBody.transform.eulerAngles.y);
                         if (topBody.transform.eulerAngles.y >= 0 && topBody.transform.eulerAngles.y < 45)
                             downBody.transform.rotation = Quaternion.Euler(0, 0, 0);
                         if (topBody.transform.eulerAngles.y >= 45 && topBody.transform.eulerAngles.y <= 90)
                             downBody.transform.rotation = Quaternion.Euler(0, 45, 0);
                         if (topBody.transform.eulerAngles.y >= 90 && topBody.transform.rotation.y < 135)
                             downBody.transform.rotation = Quaternion.Euler(0, 90, 0);
                         if (topBody.transform.eulerAngles.y >= 135 && topBody.transform.rotation.y < 180)
                             downBody.transform.rotation = Quaternion.Euler(0, 135, 0);
                         if (topBody.transform.eulerAngles.y >= 180 && topBody.transform.rotation.y < 225)
                             downBody.transform.rotation = Quaternion.Euler(0, 180, 0);
                         if (topBody.transform.eulerAngles.y >= 225 && topBody.transform.rotation.y < 270)
                             downBody.transform.rotation = Quaternion.Euler(0, 225, 0);
                         if (topBody.transform.eulerAngles.y >= 270 && topBody.transform.rotation.y < 315)
                             downBody.transform.rotation = Quaternion.Euler(0, 270, 0);
                         if (topBody.transform.eulerAngles.y >= 315 && topBody.transform.rotation.y < 360)
                             downBody.transform.rotation = Quaternion.Euler(0, 315, 0);
                         //Стрельба
                         if (Input.GetMouseButton(0))
                            {
                                NewPositionGoal = new Vector3(topBody.transform.position.x + Random.Range(-0.1f - accuracyMax, 0.1f + accuracyMax), topBody.transform.position.y + Random.Range(-0.01f - accuracyMax / 10, 0.01f + accuracyMax / 10), topBody.transform.position.z);
                                if (Physics.Raycast(NewPositionGoal, topBody.transform.forward, out hitObj, 100f))
                                {
                                    //Debug.DrawLine(topBody.transform.position, topBody.transform.forward + (goal.normal * 0.5f), Color.yellow);
												//Действия в зависимости от текущего оружия
												switch (currentW) {
												case "pistols(Clone)":
														ShotPistols ();
														break;
												case "gun(Clone)":
														ShotGun ();
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
												default :
														print("default");
														break;
										}
								}
						}
				}
        if (Input.GetMouseButtonUp(0))
        {
            //Действия в зависимости от текущего оружия
            if (currentW == "firegun(Clone)")
                ShotFiregun(false);
            if (currentW == "zeusgun(Clone)")
                ShotZeusgun(false);
            if (currentW == "plasmicgun(Clone)")
                ShotPlasmicgun(false);
        }
		}
}
