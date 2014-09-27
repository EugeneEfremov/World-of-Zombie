using UnityEngine;
using System.Collections;

public class Weapons : MonoBehaviour {
	private float delayShot; //Пауза выстрела

	public int strongMax, accuracyMax, gunBullet = 20, gunMax = 100, grenadeBullet = 20, grenadeMax = 100, minigunBullet = 1000, minigunMax = 100, rocketBullet = 20, rocketMax = 100, diskgunBullet = 20, diskgunMax = 100, firegunMax = 30, zeusgunMax = 30; //Патроны, максимальное кол-во
    public float firegunBullet = 15, zeusgunBullet = 15;
    public GameObject cam, bulletSpawn;
	public bool pistolsC = true, gunC = false, grenadeC = false, minigunC = false, rocketC = false, diskgunC = false, firegunC = false, firegunActive = false, zeusgunC = false, zeusgunActive = false; //Наличие
	public string currentW;
	public Transform pistols, gun, grenade, boom, minigun, rocket, diskgun, firegun, zeusgun; //Объекты
	public AudioClip[] pistolsA, gunA, grenadeA; //Звуки выстрелов
	public bool GBgun = false, GBgrenade = false, GBminigun = false, GBrocket = false, GBdiskgun = false, GBfiregun = false, GBzeusgun = false; //Открыто ли оружие
    public Vector3 NewPositionGoal;

    void Start()
    {
        strongMax = GetComponent<Global>().strongMax;
        accuracyMax = GetComponent<Global>().accuracyMax;

        bulletSpawn = GameObject.Find("BulletSpawn");
        cam = GameObject.Find("Camera");
        currentW = "pistols(Clone)";

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
        return bullet;
    }

	void SwitchWeapon(Transform weapon){
		Destroy (GameObject.Find(currentW));
		Transform newWeap = Instantiate (weapon, transform.position, Quaternion.Euler(0,0,0)) as Transform;
		newWeap.parent = transform; //Присвоение к объекту актера
		currentW = newWeap.transform.name;
	}
    
	RaycastHit hitObj; //Куда попал патрон
	Ray camRay; //Луч выпускаемый из прицела
	RaycastHit goal; //Куда смотрит прицел
	
//Хор-ки оружия (СОБЫТИЕ ВО ВРЕМЯ ВЫСТРЕЛА)
	void ShotPistols(){
		if (delayShot <= 0 && hitObj.transform.tag == "Zombie") {
						GameObject.Find (hitObj.transform.name).GetComponent<ZombieMove> ().helth -= 30;
						GameObject.Find ("Actor").GetComponent<Actor> ().count += 30;
				}
				if (delayShot <= 0) {
						delayShot = 0.3f;
						GetComponent<AudioSource> ().PlayOneShot (pistolsA [Random.Range (0, pistolsA.Length)]);
				}
	}

	void ShotGun(){
        gunBullet = MaxBullet(gunMax, gunBullet);

		if (delayShot <= 0 && hitObj.transform.tag == "Zombie" && gunBullet > 0) {
						GameObject.Find (hitObj.transform.name).GetComponent<ZombieMove> ().helth -= 60;
						GameObject.Find ("Actor").GetComponent<Actor> ().count += 60;
		}
		if (delayShot <= 0) {
				gunBullet--;
				delayShot = 0.8f;
		}
	}

	void ShotGrenade(){
        grenadeBullet = MaxBullet(grenadeMax, grenadeBullet);

		if (delayShot <= 0 && grenadeBullet > 0) {
			Instantiate(boom, hitObj.point, Quaternion.Euler(0,0,0));
		}
		if (delayShot <= 0) {
				grenadeBullet--;
				delayShot = 0.8f;
				GetComponent<AudioSource> ().PlayOneShot (grenadeA [0]);
		}
	}

	void ShotMinigun(){
        minigunBullet = MaxBullet(minigunMax, minigunBullet);

		if (delayShot <= 0 && hitObj.transform.tag == "Zombie" && minigunBullet > 0) {
			GameObject.Find (hitObj.transform.name).GetComponent<ZombieMove> ().helth -= 30;
			GameObject.Find ("Actor").GetComponent<Actor> ().count += 25;
		}
		if (delayShot <= 0) {
				minigunBullet--;
				delayShot = 0.1f;
		}
	}

	void ShotRocket(){
        rocketBullet = MaxBullet(rocketMax, rocketBullet);

		if (delayShot <= 0 && rocketBullet > 0) {
			Instantiate(boom, hitObj.point, Quaternion.Euler(0,0,0));
		}
		if (delayShot <= 0) {
			rocketBullet--;
			delayShot = 0.4f;
		}
	}

	void ShotDiskgun(){
        diskgunBullet = MaxBullet(diskgunMax, diskgunBullet);

		if (delayShot <= 0 && hitObj.transform.tag == "Zombie" && diskgunBullet > 0) {
			GameObject.Find (hitObj.transform.name).GetComponent<ZombieMove> ().helth -= 150;
			GameObject.Find ("Actor").GetComponent<Actor> ().count += 150;
		}
		if (delayShot <= 0) {
			diskgunBullet--;
			delayShot = 0.5f;
		}
	}

    void ShotFiregun(bool down)
    {
        if (firegunBullet > 0 && down)
        {
            firegunActive = true;
            GameObject.Find("FireZone").GetComponent<Collider>().enabled = true;
            GameObject.Find("ParticleFireGun").particleSystem.enableEmission = true;
        }
        if (firegunBullet < 0 || !down)
        {
            firegunActive = false;
            GameObject.Find("FireZone").GetComponent<Collider>().enabled = false;
            GameObject.Find("ParticleFireGun").particleSystem.enableEmission = false;
        }
    }

    void ShotZeusgun(bool down)
    {
        if (zeusgunBullet > 0 && down)
        {
            zeusgunActive = true;
            GameObject.Find("ZeusZone").GetComponent<Collider>().enabled = true;
        }
        if (zeusgunBullet < 0 || !down)
        {
            zeusgunActive = false;
            GameObject.Find("ZeusZone").GetComponent<Collider>().enabled = false;
        }
    }
//Хор-ки оружия КОНЕЦ

	void Update() {
		delayShot -= Time.deltaTime;

        if (firegunActive)
            firegunBullet -= Time.deltaTime;
        if (!firegunActive && firegunBullet < firegunMax)
            firegunBullet += Time.deltaTime;

        if (zeusgunActive)
            zeusgunBullet -= Time.deltaTime;
        if (!zeusgunActive && zeusgunBullet < zeusgunMax)
            zeusgunBullet += Time.deltaTime;

		if(Input.GetKey(KeyCode.Alpha1) && currentW != "pistols" && pistolsC){
			SwitchWeapon(pistols);
		}
		if(Input.GetKey(KeyCode.Alpha2) && currentW != "gun" && gunC){
			SwitchWeapon(gun);
		}
		if(Input.GetKey(KeyCode.Alpha3) && currentW != "grenade" && grenadeC){
			SwitchWeapon(grenade);
		}
		if(Input.GetKey(KeyCode.Alpha4) && currentW != "minigun" && minigunC){
			SwitchWeapon(minigun);
		}
		if(Input.GetKey(KeyCode.Alpha5) && currentW != "rocket" && rocketC){
			SwitchWeapon(rocket);
		}
		if(Input.GetKey(KeyCode.Alpha6) && currentW != "diskgun" && diskgunC){
			SwitchWeapon(diskgun);
		}
        if (Input.GetKey(KeyCode.Alpha7) && currentW != "firegun" && firegunC)
        {
            SwitchWeapon(firegun);
        }
        if (Input.GetKey(KeyCode.Alpha8) && currentW != "zeusgun" && zeusgunC)
        {
            SwitchWeapon(zeusgun);
        }

		//Стрельба
		if (Input.GetMouseButton (0)) {
						camRay = cam.camera.ScreenPointToRay (Input.mousePosition); //Позиция прицела
						if (Physics.Raycast (cam.transform.position, camRay.direction, out goal, 100f)) {//Куда смотрит прицел
								Debug.DrawLine (cam.transform.position, goal.point, Color.red);
			
                                //Вращение точки спавна патрон за прицелом
								Vector3 relativePos = goal.point - bulletSpawn.transform.position;
								Quaternion rotation = Quaternion.LookRotation (relativePos);
								bulletSpawn.transform.rotation = rotation;

                                NewPositionGoal = new Vector3(bulletSpawn.transform.position.x + Random.Range(-0.1f - accuracyMax, 0.1f + accuracyMax), bulletSpawn.transform.position.y + Random.Range(-0.01f - accuracyMax / 10, 0.01f + accuracyMax / 10), bulletSpawn.transform.position.z);
                                if (Physics.Raycast(NewPositionGoal, bulletSpawn.transform.forward, out hitObj, 100f))
                                {
										Debug.DrawLine (bulletSpawn.transform.position, bulletSpawn.transform.forward + (goal.normal * 0.5f), Color.yellow);
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
        }
		}
}
