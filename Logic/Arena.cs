using UnityEngine;
using System.Collections;

public class Arena : MonoBehaviour {

    public Transform  gun, cam;
    public Vector3 newRotation;
    public RaycastHit hitObj, goal;
    public Ray camRay; //Луч из прицела
    public Quaternion rotationGun;
    public float delayShot;

    void Start()
    {
        gun = GameObject.Find("Actor").transform;
        cam = GameObject.Find("Camera").transform;
    }

    void FixedUpdate()
    {
        delayShot -= Time.deltaTime;

        camRay = cam.camera.ScreenPointToRay (Input.mousePosition); //Позиция прицела
        if (Physics.Raycast(cam.position, camRay.direction, out goal, 100f)){
            //Debug.DrawLine(cam.position, goal.point, Color.red);

        //Вращение пушки
            newRotation = goal.point - gun.position;
            rotationGun = Quaternion.LookRotation(newRotation);
            gun.rotation = rotationGun;

            //Schot
            if (Input.GetMouseButton(0)){
                if (Physics.Raycast(gun.position, gun.forward, out hitObj, 100f))
                {
                    if (delayShot <= 0)
                    {
                        if (hitObj.transform.tag == "Zombie")
                        {
                            GameObject.Find(hitObj.transform.name).GetComponent<ZombieMove>().helth -= 50;
                            GameObject.Find("Actor").GetComponent<Actor>().count += 35;
                        }
                        if (hitObj.transform.tag == "Barel")
                        {
                            hitObj.transform.GetComponent<Barel>().helth -= 50;
                            GameObject.Find("Actor").GetComponent<Actor>().count += 20;
                        }
                    }
                    if (delayShot <= 0)
                        delayShot = 0.1f;
                }
            }
        }
    }
}
