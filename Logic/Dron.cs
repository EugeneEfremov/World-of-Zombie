using UnityEngine;
using System.Collections;

public class Dron : MonoBehaviour {

    private GameObject Player;

    public int helth = 100;

    RaycastHit DronHit;

    void Start()
    {
        Player = GameObject.Find("Actor");
    }

    void Update()
    {

        if (Physics.Raycast(transform.position, GameObject.FindWithTag("Zombie").transform.position, out DronHit, 10f))
        {
            Vector3 relativePos = GameObject.FindWithTag("Zombie").transform.position - transform.position;
            Quaternion rotation = Quaternion.LookRotation(relativePos);
            transform.rotation = rotation;
        }
    }

}
