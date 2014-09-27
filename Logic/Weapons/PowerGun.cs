using UnityEngine;
using System.Collections;

public class PowerGun : MonoBehaviour {

    public string type;

	void OnTriggerStay(Collider other){
        if (other.tag == "Zombie")
        {
            if (type == "firegun")
            {
                GameObject.Find(other.transform.name).GetComponent<ZombieMove>().helth -= 2;
                GameObject.Find("Actor").GetComponent<Actor>().count += 2;
            }
            if (type == "zeusgun")
            {
                GameObject.Find(other.transform.name).GetComponent<ZombieMove>().helth -= 3;
                GameObject.Find("Actor").GetComponent<Actor>().count += 3;
            }
            if (type == "plasmicgun")
            {
                GameObject.Find(other.transform.name).GetComponent<ZombieMove>().helth -= 2;
                GameObject.Find("Actor").GetComponent<Actor>().count += 2;
            }
        }
    }
}
