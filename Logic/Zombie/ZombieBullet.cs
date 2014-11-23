using UnityEngine;
using System.Collections;

public class ZombieBullet : MonoBehaviour {

    private Actor Player;

    public string type;
    float timeToDestroy;

	void Start () {
        Player = GameObject.Find("Actor").transform.GetComponent<Actor>();

        if (type == "Bandit")
            transform.rigidbody.AddForce(transform.forward * 550);
        if (type == "Forester")
            transform.rigidbody.AddForce(transform.forward * 550);
	}

    void OnTriggerEnter(Collider other)
    {
        #region Actor
        if (other.transform.name == "Actor")
        {
            if (other.GetComponent<Actor>().armour > 0)
            {
                switch (type)
                {
                    case "Bandit":
                        other.transform.GetComponent<Actor>().helth -= 1;
                        other.transform.GetComponent<Actor>().armour -= 1;
                        break;
                    case "Forester":
                        other.transform.GetComponent<Actor>().helth -= 2;
                        other.transform.GetComponent<Actor>().armour -= 2;
                        break;
                }
            }

            if (other.GetComponent<Actor>().armour < 0)
            {
                switch (type)
                {
                    case "Bandit":
                        other.transform.GetComponent<Actor>().helth -= 2;
                        break;
                    case "Forester":
                        other.transform.GetComponent<Actor>().helth -= 3;
                        break;
                }
            }
        }
        #endregion

        #region Bmp
        if (other.transform.name == "bmp")
        {
            if (Player.armour > 0)
            {
                switch (type)
                {
                    case "Bandit":
                        Player.transform.GetComponent<Actor>().helth -= 1;
                        Player.transform.GetComponent<Actor>().armour -= 1;
                        break;
                    case "Forester":
                        Player.transform.GetComponent<Actor>().helth -= 2;
                        Player.transform.GetComponent<Actor>().armour -= 2;
                        break;
                }
            }

            if (Player.armour <= 0)
            {
                switch (type)
                {
                    case "Bandit":
                        Player.transform.GetComponent<Actor>().helth -= 2;
                        break;
                    case "Forester":
                        Player.transform.GetComponent<Actor>().helth -= 3;
                        break;
                }
            }
        }
        #endregion

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
