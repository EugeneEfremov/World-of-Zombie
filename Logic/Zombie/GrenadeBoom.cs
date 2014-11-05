using UnityEngine;
using System.Collections;

public class GrenadeBoom : MonoBehaviour {

    void OnTriggerEnter(Collider other)
    {
        if (other.transform.name == "Actor")
            other.GetComponent<Actor>().helth -= 20;
    }

    void Start()
    {
        Destroy(gameObject, 0.7f);
    }
}
