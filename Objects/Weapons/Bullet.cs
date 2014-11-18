using UnityEngine;
using System.Collections;

public class Bullet : MonoBehaviour {

    public GameObject boom, boomArena, boomTexture;
    public string type;

	void Start () {
        Destroy(gameObject, 2);
	}

    void OnTriggerEnter(Collider other)
    {
        if (other.transform.name != "Actor")
        {
            //Направлять патрон по формвард и взрывать при коллизии

            if (other.transform.tag != "Boom")
            {
                if (type == "ArenaBullet")
                    Instantiate(boomArena, transform.position, Quaternion.Euler(0, 0, 0));

                if (type == "Grenade" || type == "Rocket")
                    Instantiate(boom, transform.position, Quaternion.Euler(0, 0, 0));
            }

            if (other.tag == "Zombie")
            {
                switch (type)
                {
                    case "Pistols":
                        other.GetComponent<ZombieMove>().helth -= 20;
                        break;
                    case "Gun":
                        other.GetComponent<ZombieMove>().helth -= 50;
                        break;
                    case "Minigun":
                        other.GetComponent<ZombieMove>().helth -= 30;
                        break;
                    case "Diskgun":
                        other.GetComponent<ZombieMove>().helth -= 170;
                        break;
                    case "Gaussgun":
                        other.GetComponent<ZombieMove>().helth -= 450;
                        break;
                }
            }

            if (other.tag == "Barel")
                other.GetComponent<Barel>().helth -= 100;

            Destroy(gameObject);
        }
    }
}
