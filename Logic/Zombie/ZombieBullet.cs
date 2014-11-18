using UnityEngine;
using System.Collections;

public class ZombieBullet : MonoBehaviour {

    public string type;
    public Transform GrenadeBoom;
    float timeToDestroy;

	void Start () {
        if (type == "Solders")
            transform.rigidbody.AddForce(transform.forward * 800);
        if (type == "Grenade")
            transform.rigidbody.AddForce(transform.forward * 700);
        if (type == "bZomb")
            transform.rigidbody.AddForce(transform.forward * 500);
	}

    void OnTriggerEnter(Collider other)
    {
        if (other.transform.name == "Actor")
        {
            switch (type)
            {
                case "Solders":
                    other.transform.GetComponent<Actor>().helth -= 5;
                break;
                case "bZomb":
                    other.transform.GetComponent<Actor>().helth -= 15;
                break;
                case "Grenade":
                    Instantiate(GrenadeBoom, transform.position, Quaternion.Euler(0, 0, 0));
                break;
            }
        }

        if (other.transform.name == "bmp")
        {
            switch (type)
            {
                case "Solders":
                    GameObject.Find("Actor").transform.GetComponent<Actor>().helth -= 5;
                    break;
                case "bZomb":
                    GameObject.Find("Actor").transform.GetComponent<Actor>().helth -= 15;
                    break;
            }
        }
        if (other.collider.enabled)
           Destroy(gameObject);
    }

    void FixedUpdate()
    {
        timeToDestroy += Time.deltaTime;

        if (timeToDestroy > 4)
            Destroy(gameObject);
    }
}
