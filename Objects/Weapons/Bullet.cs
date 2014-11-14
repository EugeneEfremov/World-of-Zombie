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
            /*if (type == "ArenaBullet")
                Instantiate(boomArena, position, Quaternion.Euler(0, 0, 0));

            if (type == "Grenade" || type == "Rocket")
                Instantiate(boom, position, Quaternion.Euler(0, 0, 0));*/

            Destroy(gameObject);
        }
    }
}
