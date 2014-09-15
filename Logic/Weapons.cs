using UnityEngine;
using System.Collections;

public class Weapons : MonoBehaviour {
	private float delayShot; //Пауза выстрела

	public int gunBullet = 20, grenadeBullet = 20, minigunBullet = 1000, rocketBullet = 20, diskgunBullet = 20; //Патроны
	public GameObject cam, bulletSpawn;
	public bool pistolsC = true, gunC = false, grenadeC = false, minigunC = false, rocketC = false, diskgunC = false; //Наличие
	public string currentW;
	public Transform pistols, gun, grenade, boom, minigun, rocket, diskgun; //Объекты
	public AudioClip[] pistolsA, gunA, grenadeA; //Звуки выстрелов
	public bool GBgun = false, GBgrenade = false, GBminigun = false, GBrocket = false, GBdiskgun = false; //Открыто ли оружие

	void Start(){
		bulletSpawn = GameObject.Find ("BulletSpawn");
		cam = GameObject.Find("Camera");
		currentW = "pistols(Clone)";
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
		if (delayShot <= 0 && rocketBullet > 0) {
			Instantiate(boom, hitObj.point, Quaternion.Euler(0,0,0));
		}
		if (delayShot <= 0) {
			rocketBullet--;
			delayShot = 0.4f;
		}
	}

	void ShotDiskgun(){
		if (delayShot <= 0 && hitObj.transform.tag == "Zombie" && diskgunBullet > 0) {
			GameObject.Find (hitObj.transform.name).GetComponent<ZombieMove> ().helth -= 150;
			GameObject.Find ("Actor").GetComponent<Actor> ().count += 150;
		}
		if (delayShot <= 0) {
			diskgunBullet--;
			delayShot = 0.5f;
		}
	}
//Хор-ки оружия КОНЕЦ

	void Update() {
		delayShot -= Time.deltaTime;

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

		//Стрельба
		if (Input.GetMouseButton (0)) {
						camRay = cam.camera.ScreenPointToRay (Input.mousePosition); //Позиция прицела
						if (Physics.Raycast (cam.transform.position, camRay.direction, out goal, 100f)) {//Куда смотрит прицел
								Debug.DrawLine (cam.transform.position, goal.point, Color.red);
			
								Vector3 relativePos = goal.point - bulletSpawn.transform.position;
								Quaternion rotation = Quaternion.LookRotation (relativePos);
								bulletSpawn.transform.rotation = rotation;
			
								if (Physics.Raycast (bulletSpawn.transform.position, bulletSpawn.transform.forward, out hitObj, 100f)) {
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
												default :
														print("default");
														break;
										}
								}
						}
				}
		}
}
