using UnityEngine;
using System.Collections;

public class Arena : MonoBehaviour {

    private float timeRecoveryHelth = 0, newTimeRecoveryHelth = 0;
    private int helthActor, maxHelthActor, oldBulletSpawn = 1, _creatingShellBullet;
    private Transform newBullet, newShellBullet;
    private Quaternion rotation;

    public Transform gun, cam, boomArena, bullet, sheelBullet, bulletSpawn1, shotParticle1, bulletSpawn2, shotParticle2, sheelBulletSpawn;
    public RaycastHit goal;
    public Ray camRay; //Луч из прицела
    public float delayShot;

    void Start()
    {
        gun = GameObject.Find("Actor").transform;
        cam = GameObject.Find("Camera").transform;
        maxHelthActor = gun.GetComponent<Actor>().helthMax;

        _creatingShellBullet = PlayerPrefs.GetInt("shellBullet");
    }
    void FixedUpdate()
    {
        //Восстановление здоровья Актера
        helthActor = gun.GetComponent<Actor>().helth;

        if (helthActor < maxHelthActor && timeRecoveryHelth <= 25)
            timeRecoveryHelth += Time.deltaTime;

        if (helthActor == maxHelthActor)
            timeRecoveryHelth = 0;

        if (timeRecoveryHelth >= 25)
        {
            newTimeRecoveryHelth += Time.deltaTime;
            if (newTimeRecoveryHelth >= 5)
            {
                gun.GetComponent<Actor>().helth += 20;
                newTimeRecoveryHelth = 0;
            }
        }
        //КОНЕЦ Восстановление здоровья Актера

        delayShot -= Time.deltaTime;

        camRay = cam.camera.ScreenPointToRay (Input.mousePosition); //Позиция прицела
        if (Physics.Raycast(cam.position, camRay.direction, out goal, 100f) && !gun.GetComponent<Actor>().death)
        {

        //Вращение пушки
            rotation = Quaternion.LookRotation(goal.point - gun.position);
            gun.rotation = Quaternion.Euler(270, rotation.eulerAngles.y + 90, rotation.eulerAngles.z);

            if (delayShot <= 2)
            {
                shotParticle1.particleSystem.enableEmission = false;
                shotParticle2.particleSystem.enableEmission = false;
            }

            //Schot
            if (delayShot <= 0)
              {
                   if (Input.GetMouseButton(0))
                      {
                            delayShot = 0.4f;
                            //Выбор точки спавна ракеты
                            if (oldBulletSpawn == 1)
                            {
                                shotParticle2.particleSystem.enableEmission = true;
                                newBullet = Instantiate(bullet, bulletSpawn2.position, Quaternion.Euler(rotation.eulerAngles.x + 90, rotation.eulerAngles.y, rotation.eulerAngles.z)) as Transform;
                                oldBulletSpawn = 2;
                            }
                            else if (oldBulletSpawn != 1)
                            {
                                shotParticle1.particleSystem.enableEmission = true;
                                newBullet = Instantiate(bullet, bulletSpawn1.position, Quaternion.Euler(rotation.eulerAngles.x + 90, rotation.eulerAngles.y, rotation.eulerAngles.z)) as Transform;
                                oldBulletSpawn = 1;
                            }
                            //Свойства новой ракеты
                            newBullet.GetComponent<Bullet>().position = goal.point;
                            newBullet.rigidbody.AddForce(newBullet.up * 1000);
                            
                            //Свойства новой гильзы
                            if (_creatingShellBullet == 1)
                            {
                                newShellBullet = Instantiate(sheelBullet, sheelBulletSpawn.transform.position, transform.rotation) as Transform;
                                newShellBullet.rigidbody.AddForce(newShellBullet.right * 2);
                            }

                            //Запасной вариант (убиство лучем)
                            if (goal.transform.tag == "Zombie")
                            {
                                GameObject.Find(goal.transform.name).GetComponent<ZombieMove>().helth -= 70;
                                GameObject.Find("Actor").GetComponent<Actor>().count += 50;
                            }
                            if (goal.transform.tag == "Barel")
                            {
                                goal.transform.GetComponent<Barel>().helth -= 100;
                                GameObject.Find("Actor").GetComponent<Actor>().count += 30;
                            }
                        }
            }
        }
    }
}
