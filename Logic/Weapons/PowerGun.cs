using UnityEngine;
using System.Collections;

public class PowerGun : MonoBehaviour {

    private float distanse;
    private RaycastHit hit;

    public bool shot = false;
    public string type;

    void Start(){
        switch(type){
            case "firegun":
                distanse = 10f;
                break;
            case "zeusgun":
                distanse = 10f;
                break;
            case "plasmicgun":
                distanse = 20f;
                break;
        }
    }

    void FixedUpdate()
    {
       if (Physics.Raycast(transform.position, -transform.forward, out hit, distanse) && shot)
        {
            if (hit.rigidbody)
                hit.rigidbody.AddForce(Vector3.forward * 10);
        }
    }

	void OnTriggerStay(Collider other){
        switch (other.transform.tag){
            case "Zombie":
                if (type == "firegun" && shot)
                {
                    GameObject.Find(other.transform.name).GetComponent<ZombieMove>().helth -= 2;
                    GameObject.Find("Actor").GetComponent<Actor>().count += 2;
                }
                if (type == "zeusgun" && shot)
                {
                    GameObject.Find(other.transform.name).GetComponent<ZombieMove>().helth -= 3;
                    GameObject.Find("Actor").GetComponent<Actor>().count += 3;
                }
                if (type == "plasmicgun" && shot)
                {
                    GameObject.Find(other.transform.name).GetComponent<ZombieMove>().helth -= 2;
                    GameObject.Find("Actor").GetComponent<Actor>().count += 2;
                }
            break;

            case "Barel":
            if (type == "firegun" && shot)
                {
                      GameObject.Find(other.transform.name).GetComponent<Barel>().helth -= 20;
                      GameObject.Find("Actor").GetComponent<Actor>().count += 20;
                }
            if (type == "zeusgun" && shot)
                {
                        GameObject.Find(other.transform.name).GetComponent<Barel>().helth -= 30;
                     GameObject.Find("Actor").GetComponent<Actor>().count += 30;
                }
            if (type == "plasmicgun" && shot)
                 {
                     GameObject.Find(other.transform.name).GetComponent<Barel>().helth -= 20;
                     GameObject.Find("Actor").GetComponent<Actor>().count += 20;
                 }
            break;
        }
    }
}
