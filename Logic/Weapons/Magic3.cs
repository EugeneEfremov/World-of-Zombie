using UnityEngine;
using System.Collections;

public class Magic3 : MonoBehaviour {


    void OnTriggerStay(Collider other)
    {
        if (other.transform.tag == "Zombie")
        {
            other.GetComponent<ZombieMove>().helth -= 250;
            GameObject.Find("Actor").GetComponent<Actor>().count += 110;
        }
    }

    void Start()
    {
        Destroy(gameObject, 5f);
    }
}
